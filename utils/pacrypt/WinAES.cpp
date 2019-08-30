#include "StdAfx.h"

#include "WinAES.h"
#include <fstream>
#include <sstream>
#include "assert.h"
#include <exception>
#include <vector>
#include <certenroll.h>
#include <atlbase.h>

using std::ostringstream;
using std::fstream;

WinAES::WinAES( const wchar_t* lpszContainer, int nFlags )
	: m_hProvider(NULL), m_hAesKey(NULL), m_bHaveIv(false),
	m_nFlags(nFlags), m_wszContainer(NULL), m_nIndex(INVALID_INDEX)
{
	if( NULL == lpszContainer ) {
		lpszContainer = L"Temporary - OK to Delete";
		m_nFlags |= DELETE_CONTAINER;
	}

	size_t len = wcslen( lpszContainer ) + 1;
	assert( 0 != len );

	if( len ) {
		m_wszContainer = new wchar_t[ len ];
		assert( NULL != m_wszContainer );

		errno_t err = 0;
		err = wcscpy_s( m_wszContainer, len, lpszContainer );
		assert( 0 == err );
	}

	bool b = AcquireContext( m_wszContainer );
	assert( true == b );
}

WinAES::WinAES( HCRYPTPROV hProvider )
	: m_hProvider(hProvider), m_hAesKey(NULL), m_bHaveIv(false),
	m_nFlags(DEFAULT_FLAGS), m_wszContainer(NULL), m_nIndex(INVALID_INDEX) { }

WinAES::~WinAES( )
{
	// Destroy the key
	if( NULL != m_hAesKey ) {
		CryptDestroyKey( m_hAesKey );
		m_hAesKey = NULL;
	}

	// Destroy the provider
	if( NULL != m_hProvider ) {
		CryptReleaseContext( m_hProvider, 0 );
		m_hProvider = NULL;
	}

	// Destroy the conatiner
	if( DELETE_CONTAINER & m_nFlags )
	{
		if( INVALID_INDEX != m_nIndex )
		{       
			// MSDN: When CRYPT_DELETEKEYSET flag is set, the value returned in phProv
			// is undefined, and thus, the CryptReleaseContext function need not be
			// called afterward.
			CryptAcquireContext( &m_hProvider, m_wszContainer, AesProviders[m_nIndex].params.lpwsz,
				AesProviders[m_nIndex].params.dwType, CRYPT_DELETEKEYSET );
		}
		m_hProvider = NULL;
	}
	m_nIndex = INVALID_INDEX;

	// Free
	if( NULL != m_wszContainer ) {
		delete[] m_wszContainer;
		m_wszContainer = NULL;
	}
}

bool WinAES::GenerateRandom( byte* buffer, int size )
{
	assert( NULL != buffer );
	assert( size >= 0 );

#ifdef _DEBUG
	// Test the pointer (it is purported good). Not safe for Release builds
	assert( FALSE == IsBadWritePtr( buffer, size ) );
#endif

	assert( NULL != m_hProvider );

	// Convert big BOOL to little bool
	return !!CryptGenRandom( m_hProvider, size, buffer );
}

void enumProviders(void)
{
	CComPtr<ICspInformations>     pCSPs;   // Provider collection
	CComPtr<ICspInformation>      pCSP;    // Provider instgance
	HRESULT           hr = S_OK;  // Return value
	long              lCount = 0;     // Count of providers
	CComBSTR          bstrName;            // Provider name
	VARIANT_BOOL      bLegacy;             // CryptoAPI or CNG	
	// Initialize COM.
	hr = CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);
	if (FAILED(hr)) {
		cerr << "Could not initialize COM" << endl;
		return;
	}

	// Create a collection of cryptographic providers.
	hr = CoCreateInstance(
		__uuidof(CCspInformations),
		NULL,
		CLSCTX_INPROC_SERVER,
		__uuidof(ICspInformations),
		(void **)&pCSPs);
	if (FAILED(hr)) {
		cerr << "Could not Create the collection of cryptographic providers" << endl;
		return;
	}

	// Add the providers installed on the computer.
	hr = pCSPs->AddAvailableCsps();
	if (FAILED(hr)) {
		cerr << "Could not add providers installed on this computer" << endl;
		return;
	}

	// Retrieve the number of installed providers.
	hr = pCSPs->get_Count(&lCount);
	if (FAILED(hr)) {
		cerr << "Could not retrieve the number of installed providers" << endl;
		return;
	}

	// Print the providers to the console. Print the
	// name and a value that specifies whether the 
	// CSP is a legacy or CNG provider.
	for (long i = 0; i<lCount; i++)
	{
		hr = pCSPs->get_ItemByIndex(i, &pCSP);
		if (FAILED(hr)) return;

		hr = pCSP->get_Name(&bstrName);
		if (FAILED(hr)) return;

		hr = pCSP->get_LegacyCsp(&bLegacy);
		if (FAILED(hr)) return;

		if (VARIANT_TRUE == bLegacy)
			std::wcerr << i << L". Legacy: ";
		else
			std::wcerr << i << L". CNG: ";

		std::wcerr << static_cast<wchar_t*>(bstrName.m_str) << endl;

		pCSP = NULL;

	}

	CoUninitialize();
}

bool WinAES::AcquireContext( const wchar_t* lpszContainer )
{
	// We need to record the index of the provider which we were able to 
	// acquire in case the use wants to delete the imported key. The
	// class destructor will use the index to delete the proper container.

	for (int i = 0; i < _countof(AesProviders); i++)
	{
		// Only create a new container if requested
		if ((AesProviders[i].params.dwFlags & CRYPT_NEWKEYSET) &&
			!(m_nFlags & CREATE_CONTAINER)) {
			continue;
		}

		if (CryptAcquireContext(&m_hProvider, lpszContainer, AesProviders[i].params.lpwsz,
			AesProviders[i].params.dwType, AesProviders[i].params.dwFlags)) {
			m_nIndex = i;
			break;
		}
		else {
			int lastError = GetLastError();
			if (lastError == NTE_EXISTS || lastError == NTE_KEYSET_ENTRY_BAD) {
				CryptAcquireContext(&m_hProvider, lpszContainer, AesProviders[i].params.lpwsz,
					AesProviders[i].params.dwType, CRYPT_DELETEKEYSET);
				if (CryptAcquireContext(&m_hProvider, lpszContainer, AesProviders[i].params.lpwsz,
					AesProviders[i].params.dwType, AesProviders[i].params.dwFlags)) {
					m_nIndex = i;
					break;
				}

			}
			cerr << "Could not acquire context for ";
			std::wcerr << AesProviders[i].params.lpwsz;
			cerr << " : " << ErrorToDefine(GetLastError());
			cerr << " (0x" << std::hex << GetLastError() << ")" << endl;
		}
	}

	// m_nIndex != INVALID_INDEX indicates success. No need for an
	//  extra variable to track success from CryptAcquireContext.
	if( (m_nIndex == INVALID_INDEX) && (THROW_EXCEPTION & m_nFlags) ) {
		throw WinAESException("AcquireContext: CryptAcquireContext failed");
	}
	else if(m_nIndex == INVALID_INDEX) {
		cerr << "AcquireContext: CryptAcquireContext failed" << endl;
		cerr << "Installed Providers:" << endl;
		enumProviders();
	}

	return m_nIndex != INVALID_INDEX;
}

HCRYPTPROV WinAES::DuplicateContext()
{
	assert( NULL != m_hProvider );
	if( NULL == m_hProvider ) { return NULL; }

	if( CryptContextAddRef( m_hProvider, NULL, 0 ) )
	{ return m_hProvider; }

	assert( false );
	return NULL;
}

bool WinAES::SetKeyWithIv( const byte* key, int ksize, const byte* iv, int vsize )
{
#ifdef _DEBUG
	assert( FALSE == IsBadReadPtr( key, ksize ) );
	assert( FALSE == IsBadReadPtr( iv, vsize ) );
#endif

	// Returned to caller (if non-throwing)
	bool result = true;

	// Is someone is re-keying, we need to release the old key here...
	if( NULL != m_hAesKey ) {
		// Non-fatal
		CryptDestroyKey( m_hAesKey );
		m_hAesKey  = NULL;
		m_bHaveIv = false;
	}

	try
	{
		assert( NULL != m_hProvider );
		if( NULL == m_hProvider ) {
			throw WinAESException( "SetKeyWithIv: Provider is not valid" );
		}

		assert( NULL != key );
		if( NULL == key ) {
			throw WinAESException( "SetKeyWithIv: Key buffer is not valid" );
		}

		assert( ksize == KEYSIZE_128 || ksize == KEYSIZE_192 || ksize == KEYSIZE_256 );
		if( !(ksize == KEYSIZE_128 || ksize == KEYSIZE_192 || ksize == KEYSIZE_256) ) {
			throw WinAESException( "SetKeyWithIv: Key size is not valid" );
		}

		assert( NULL != iv );
		if( NULL == iv ) {
			throw WinAESException( "SetKeyWithIv: IV buffer is not valid" );
		}

		assert( BLOCKSIZE == vsize );
		if( BLOCKSIZE != vsize ) {
			throw WinAESException( "SetKeyWithIv: IV size is not valid" );
		}

		result &= SetKey( key, ksize );
		assert( true == result );
		if( !result ) {
			throw WinAESException("SetKeyWithIv: SetKey failed");
		}

		result &= SetIv( iv, vsize );
		assert( true == result );
		if( !result ) {
			throw WinAESException("SetKeyWithIv: SetIv failed");
		}
	}
	catch( const WinAESException& e )
	{
		if( THROW_EXCEPTION & m_nFlags ) {
			throw e;
		}
		else {
			cerr << "Exception: " << e.what() << endl;
		}

		result = false;
	}

	return result;
}

// Sets the Key. The key must be of size KEYSIZE_128,
// KEYSIZE_192 or KEYSIZE_256.
bool WinAES::SetKey( const byte* key, int ksize )
{
#ifdef _DEBUG
	assert( FALSE == IsBadReadPtr( key, ksize ) );
#endif

	// Returned to caller (if non-throwing)
	bool result = false;

	// Is someone is re-keying, we need to release the old key here...
	if( NULL != m_hAesKey ) {
		// Non-fatal
		CryptDestroyKey( m_hAesKey );
		m_hAesKey  = NULL;
		m_bHaveIv = false;
	}

	try
	{
		assert( NULL != m_hProvider );
		if( NULL == m_hProvider ) {
			throw WinAESException( "SetKey: Provider is not valid" );
		}

		assert( NULL != key );
		if( NULL == key ) {
			throw WinAESException( "SetKey: Key buffer is NULL" );
		}

		assert( ksize == KEYSIZE_128 || ksize == KEYSIZE_192 || ksize == KEYSIZE_256 );
		if( !(ksize == KEYSIZE_128 || ksize == KEYSIZE_192 || ksize == KEYSIZE_256) ) {
			throw WinAESException( "SetKey: Key size is not valid" );
		}

		AesKey aeskey;

		switch( ksize )
		{
		case KEYSIZE_128:
			aeskey.Header.aiKeyAlg = CALG_AES_128;
			aeskey.dwKeyLength = KEYSIZE_128;
			break;
		case KEYSIZE_192:
			aeskey.Header.aiKeyAlg = CALG_AES_192;
			aeskey.dwKeyLength = KEYSIZE_192;
			break;
		case KEYSIZE_256:
			aeskey.Header.aiKeyAlg = CALG_AES_256;
			aeskey.dwKeyLength = KEYSIZE_256;
			break;
		default:
			assert( false );
			throw WinAESException("SetKey: Key size is not valid");
		}

		errno_t err = 0;
		err = memcpy_s( aeskey.cbKey, aeskey.dwKeyLength, key, ksize );
		assert( 0 == err );
		if( 0 != err ) {
			throw WinAESException( "SetKey: Unable to copy key" );
		}

		// When the key is imported, the size of the AesKey structure should be exact.
		//  We declared aeskey.cbKey[ KEYSIZE_256 ] to allow a stack allocation.
		//  There are enough bytes available for AES 128, 192, and 256. We do the
		//  proper book keeping now.
		const unsigned structsize = sizeof(AesKey) - KEYSIZE_256 + ksize;

		// Import AES key
		if(!CryptImportKey(m_hProvider, (CONST BYTE*)&aeskey, structsize, NULL, 0, &m_hAesKey ) ) {
			assert( false );
			throw WinAESException("SetKey: Import key failed");
		}

		// Set Mode
		DWORD dwMode = CRYPT_MODE_CBC;
		if(!CryptSetKeyParam(m_hAesKey, KP_MODE, (BYTE*)&dwMode, 0)) {
			assert( false );
			throw WinAESException("SetKey: Set CBC mode failed");
		}

		result = true;
	}
	catch( const WinAESException& e )
	{
		if( THROW_EXCEPTION & m_nFlags ) {
			throw e;
		}
		else {
			cerr << "Exception: " << e.what() << endl;
		}
		result = false;
	}

	return result;
}

// Sets the IV. The IV must be of size BLOCKSIZE.
bool WinAES::SetIv(const byte* iv, int vsize )
{
#ifdef _DEBUG
	assert( FALSE == IsBadReadPtr( iv, vsize ) );
#endif

	// Returned to caller (if non-throwing)
	bool result = false;

	try
	{
		assert( NULL != m_hAesKey );
		if( NULL == m_hAesKey ) {
			throw WinAESException( "SetIv: key is not valid" );
		}

		assert( NULL != iv );
		if( NULL == iv ) {
			throw WinAESException( "SetIv: IV buffer is NULL" );
		}

		assert( BLOCKSIZE == vsize );
		if( BLOCKSIZE != vsize ) {
			throw WinAESException( "SetIv: IV block size is not valid" );
		}

		assert( NULL != m_hAesKey );
		if( NULL == m_hAesKey ) {
			throw WinAESException( "SetIv: Key is not valid" );
		}

		if(!CryptSetKeyParam(m_hAesKey, KP_IV, (BYTE*)iv, 0)) {
			throw WinAESException( "SetIv: Set IV failed" );
		}

		// Set Mode
		DWORD dwMode = CRYPT_MODE_CBC;
		if(!CryptSetKeyParam(m_hAesKey, KP_MODE, (BYTE*)&dwMode, 0)) {
			assert( false );
			throw WinAESException("SetIv: Set CBC mode failed");
		}

		result = m_bHaveIv = true;
	}
	catch( const WinAESException& e )
	{
		result = m_bHaveIv = false;

		if( THROW_EXCEPTION & m_nFlags ) {
			throw e;
		}
		else {
			cerr << "Exception: " << e.what() << endl;
		}
	}

	return result;
}

// Returns the maximum size of the ciphertext, which includes
// padding for the plaintext
bool WinAES::MaxCipherTextSize( size_t psize, size_t& csize )
{
	size_t blocks = psize / BLOCKSIZE + 1;

	csize = blocks * BLOCKSIZE;
	return true;
}

// Returns the maximum size of the plaintext, which includes
// removal of padding on the plaintext
bool WinAES::MaxPlainTextSize( size_t csize, size_t& psize )
{
	assert( 0 == csize % BLOCKSIZE );
	if( 0 != csize % BLOCKSIZE ) return false;

	// More correctly, csize-1
	psize = csize;
	return true;
}

// Encrpyt a buffer in-place. bsize is the size of the buffer,
//  psize is the size of the plaintext. If successful,
//  csize is the size of the ciphertext. On entry, bsize >= csize.
bool WinAES::Encrypt( byte* buffer, /*In*/size_t bsize, /*In*/size_t psize, /*Out*/ size_t& csize )
{
#ifdef _DEBUG
	// Test the pointer (it is purported good). Not safe for Release builds
	assert( FALSE == IsBadWritePtr( buffer, (DWORD)bsize ) );
#endif

	// Returned to caller (if non-throwing)
	BOOL result = FALSE;

	try
	{
		// sanity check
		assert( NULL != m_hAesKey );
		if( NULL == m_hAesKey ) {
			SetLastError( (DWORD)NTE_BAD_KEY );
			throw WinAESException( "Encrypt(1): Key is not valid" );
		}

		// sanity check
		assert( NULL != buffer );
		if( NULL == buffer ) {
			SetLastError( ERROR_INVALID_USER_BUFFER );
			throw WinAESException( "Encrypt(1): Buffer is NULL" );
		}

		// sanity check
		size_t s = 0;
		if( MaxCipherTextSize( psize, s ) && bsize < s ) {
			assert( false );
			SetLastError( (DWORD)NTE_BUFFER_TOO_SMALL );
			throw WinAESException( "Encrypt(1): Buffer is too small" );
		}

		// sanity check
		assert( true == m_bHaveIv );
		if( false == m_bHaveIv ) {
			SetLastError( (DWORD)NTE_INVALID_PARAMETER );
			throw WinAESException( "Encrypt(1): IV has not been set" );
		}

		// temporary for API
		DWORD d = (DWORD)psize;
		result = CryptEncrypt( m_hAesKey, NULL, TRUE, 0, buffer, &d, (DWORD)bsize );
		assert( TRUE == result );
		if( FALSE == result ) {          
			// Build a useful message
			ostringstream emessage;
			emessage << "Encrypt(1): CryptEncrypt failed - " ;
			emessage << ErrorToDefine( GetLastError() );
			emessage << " (0x" << std::hex << GetLastError() << ")";

			csize = 0;
			throw WinAESException( emessage.str().c_str() );
		}

		// result is TRUE
		csize = d;
	}
	catch( const WinAESException& e )
	{
		if( m_nFlags & THROW_EXCEPTION ) {
			throw e;
		}
		else {
			cerr << "Exception: " << e.what() << endl;
		}
		result = FALSE;
	}

	// Convert the big BOOL to little bool
	return !!result;
}

// Decrpyt a buffer in-place. bsize is the size of the buffer,
//  csize is the size of the ciphertext. If successful,
//  psize is the size of the recovered plaintext.
//  On entry, bsize > psize (always the case since csize <= bsize).
bool WinAES::Decrypt( byte* buffer, /*In*/size_t bsize, /*In*/size_t csize, /*Out*/ size_t& psize )
{
#ifdef _DEBUG
	// Test the pointer (it is purported good). Not safe for Release builds
	assert( FALSE == IsBadWritePtr( buffer, (DWORD)bsize ) );
#endif

	// Returned to caller (if non-throwing)
	BOOL result = FALSE;

	try
	{
		// sanity check
		assert( NULL != m_hAesKey );
		if( NULL == m_hAesKey ) {
			SetLastError( (DWORD)NTE_BAD_KEY );
			throw WinAESException( "Decrypt(1): Key is not valid" );
		}

		// sanity check
		assert( NULL != buffer );
		if( NULL == buffer ) {
			SetLastError( ERROR_INVALID_USER_BUFFER );
			throw WinAESException( "Decrypt(1): Buffer is NULL" );
		}

		// sanity check
		assert( 0 == csize % BLOCKSIZE );
		if( !(0 == csize % BLOCKSIZE) ) {
			SetLastError( (DWORD)NTE_BAD_DATA );
			throw WinAESException( "Decrypt(1): Data size is not a multple of block size" );
		}

		// sanity check
		size_t s = 0;
		if( MaxPlainTextSize( csize, s ) && !(bsize > s-BLOCKSIZE) ) {
			assert( false );
			SetLastError( (DWORD)NTE_BUFFER_TOO_SMALL );
			throw WinAESException( "Decrypt(1): Buffer is too small" );
		}

		// sanity check
		assert( true == m_bHaveIv );
		if( false == m_bHaveIv ) {
			SetLastError( (DWORD)NTE_INVALID_PARAMETER );
			throw WinAESException( "Decrypt(1): IV has not been set" );
		}

		// Temporary for API
		DWORD d = (DWORD)csize;
		result = CryptDecrypt( m_hAesKey, NULL, TRUE, 0, buffer, &d );
		assert( TRUE == result );
		if( FALSE == result ) {
			// Build a useful message
			ostringstream emessage;
			emessage << "Decrypt(1): CryptDecrypt failed - " ;
			emessage << ErrorToDefine( GetLastError() );
			emessage << " (0x" << std::hex << GetLastError() << ")";

			psize = 0;
			throw WinAESException( emessage.str().c_str() );
		}

		// result is TRUE
		psize = d;
	}
	catch( const WinAESException& e )
	{
		if( m_nFlags & THROW_EXCEPTION ) {
			throw e;
		}
		else {
			cerr << "Exception: " << e.what() << endl;
		}
		result = FALSE;
	}

	// Convert the big BOOL to little bool
	return !!result;
}

// Encrypt plaintext. psize is the size of the plaintext.
//  If successful, csize is the size of the ciphertext.
bool WinAES::Encrypt( const byte* plaintext, /*In*/size_t psize, /*InOut*/byte* ciphertext, /*InOut*/ size_t& csize )
{

#ifdef _DEBUG
	// Test the pointer (it is purported good). Not safe for Release builds
	assert( FALSE == IsBadReadPtr( plaintext, psize ));
	assert( FALSE == IsBadWritePtr( ciphertext, csize ));
#endif

	// Returned to caller (if non-throwing)
	bool result = false;

	try
	{
		// sanity check
		assert( plaintext != NULL || ( plaintext == NULL && 0 == psize ) );
		if( !(plaintext != NULL || ( plaintext == NULL && 0 == psize )) ) {
			SetLastError( ERROR_INVALID_USER_BUFFER );
			throw WinAESException( "Encrypt(2): Plain text buffer is not valid" );
		}

		// sanity check
		assert( NULL != ciphertext );
		if( NULL == ciphertext ) {
			SetLastError( ERROR_INVALID_USER_BUFFER );
			throw WinAESException( "Encrypt(2): Cipher text buffer is not valid" );
		}

		// Buffers cannot overlap
		assert( ((size_t)ciphertext+csize < (size_t)plaintext) ||
			((size_t)plaintext+psize < (size_t)ciphertext) );
		if( !(((size_t)ciphertext+csize < (size_t)plaintext) ||
			((size_t)plaintext+psize < (size_t)ciphertext) ) )
		{
			SetLastError( (DWORD)NTE_BUFFERS_OVERLAP );
			throw WinAESException( "Encrypt(2): Buffers overlap" );
		}

		errno_t err = memcpy_s( ciphertext, csize, plaintext, psize );
		assert( 0 == err );
		if( 0 != err ) {
			throw WinAESException( "Encrypt(2): Unable to prepare plaintext buffer" );
		}

		result = Encrypt( ciphertext, csize, psize, csize );
		assert( true == result );
		if( FALSE == result ) {
			// Build a useful message
			ostringstream emessage;
			emessage << "Encrypt(2): Encryption failed - " ;
			emessage << ErrorToDefine( GetLastError() );
			emessage << " (0x" << std::hex << GetLastError() << ")";

			csize = 0;
			throw WinAESException( emessage.str().c_str() );
		}

		// result is TRUE
	}

	catch( const WinAESException& e )
	{
		if( m_nFlags & THROW_EXCEPTION ) {
			throw e;
		}
		else {
			cerr << "Exception: " << e.what() << endl;
		}
		result = false;
	}

	return result;
}

// Decrypt plaintext. csize is the size of the ciphertext.
//  If successful, psize is the size of the plaintext.
bool WinAES::Decrypt( const byte* ciphertext, /*In*/size_t csize, /*InOut*/byte* plaintext, /*InOut*/ size_t& psize )
{

#ifdef _DEBUG
	// Test the pointer (it is purported good). Not safe for Release builds
	assert( FALSE == IsBadReadPtr( ciphertext, csize ));
	assert( FALSE == IsBadWritePtr( plaintext, psize ));
#endif

	// Returned to caller
	bool result = false;

	try
	{
		// sanity check
		assert( NULL != ciphertext );
		assert( NULL != plaintext );
		if( NULL == ciphertext || NULL == plaintext ) {
			SetLastError( ERROR_INVALID_USER_BUFFER );
			throw WinAESException( "Decrypt(2): Buffer is NULL" );
		}

		// Buffers cannot overlap
		assert( ((size_t)ciphertext+csize < (size_t)plaintext) ||
			((size_t)plaintext+psize < (size_t)ciphertext) );
		if( !(((size_t)ciphertext+csize < (size_t)plaintext) ||
			((size_t)plaintext+psize < (size_t)ciphertext) ) )
		{
			SetLastError( (DWORD)NTE_BUFFERS_OVERLAP );
			throw WinAESException( "Decrypt(2): Buffers overlap" );
		}

		errno_t err = memcpy_s( plaintext, psize, ciphertext, csize );
		assert( 0 == err );
		if( 0 != err ) {
			throw WinAESException( "Decrypt(2): Unable to prepare decryption buffer" );
		}

		result = Decrypt( plaintext, csize, csize, psize );
		assert( true == result );
		if( false == result ) {
			// Build a useful message
			ostringstream emessage;
			emessage << "Decrypt(2): Decryption failed - " ;
			emessage << ErrorToDefine( GetLastError() );
			emessage << " (0x" << std::hex << GetLastError() << ")";

			psize = 0;
			throw WinAESException( emessage.str().c_str() );
		}

		// result is TRUE
	}

	catch( const WinAESException& e )
	{
		if( m_nFlags & THROW_EXCEPTION ) {
			throw e;
		}
		else {
			cerr << "Exception: " << e.what() << endl;			
		}
		result = false;
	}

	return result;
}

const char* WinAES::ErrorToDefine( DWORD dwError )
{
	switch( dwError )
	{
	case ERROR_ACCESS_DENIED:
		return "ERROR_ACCESS_DENIED";  
	case ERROR_INVALID_HANDLE:
		return "ERROR_INVALID_HANDLE";  
	case ERROR_INVALID_PARAMETER:
		return "ERROR_INVALID_PARAMETER";
	case ERROR_DEV_NOT_EXIST:
		return "ERROR_DEV_NOT_EXIST"; 
	case NTE_BAD_UID:
		return "NTE_BAD_UID";
	case NTE_BAD_HASH:
		return "NTE_BAD_HASH";      
	case NTE_BAD_KEY:
		return "NTE_BAD_KEY";
	case NTE_BAD_LEN:
		return "NTE_BAD_LEN";
	case NTE_BAD_DATA:
		return "NTE_BAD_DATA";
	case NTE_BAD_SIGNATURE:
		return "NTE_BAD_SIGNATURE";	
	case NTE_BAD_VER:
		return "NTE_BAD_VER";
	case NTE_BAD_ALGID:
		return "NTE_BAD_ALGID";
	case NTE_BAD_FLAGS:
		return "NTE_BAD_FLAGS";
	case NTE_BAD_TYPE:
		return "NTE_BAD_TYPE";
	case NTE_BAD_KEY_STATE:
		return "NTE_BAD_KEY_STATE";
	case NTE_BAD_HASH_STATE:
		return "NTE_BAD_HASH_STATE";
	case NTE_NO_KEY:
		return "NTE_NO_KEY";
	case NTE_NO_MEMORY:
		return "NTE_NO_MEMORY";
	case NTE_EXISTS:
		return "NTE_EXISTS";
	case NTE_PERM:
		return "NTE_PERM";
	case NTE_NOT_FOUND:
		return "NTE_NOT_FOUND";
	case NTE_DOUBLE_ENCRYPT:
		return "NTE_DOUBLE_ENCRYPT";
	case NTE_BAD_PROVIDER:
		return "NTE_BAD_PROVIDER";
	case NTE_BAD_PROV_TYPE:
		return "NTE_BAD_PROV_TYPE";
	case NTE_BAD_PUBLIC_KEY:
		return "NTE_BAD_PUBLIC_KEY";
	case NTE_BAD_KEYSET:
		return "NTE_BAD_KEYSET";
	case NTE_PROV_TYPE_NOT_DEF:
		return "NTE_PROV_TYPE_NOT_DEF";
	case NTE_PROV_TYPE_ENTRY_BAD:
		return "NTE_PROV_TYPE_ENTRY_BAD";
	case NTE_KEYSET_NOT_DEF:
		return "NTE_KEYSET_NOT_DEF";
	case NTE_KEYSET_ENTRY_BAD:
		return "NTE_KEYSET_ENTRY_BAD";
	case NTE_PROV_TYPE_NO_MATCH:
		return "NTE_PROV_TYPE_NO_MATCH";
	case NTE_SIGNATURE_FILE_BAD:
		return "NTE_SIGNATURE_FILE_BAD";
	case NTE_PROVIDER_DLL_FAIL:
		return "NTE_PROVIDER_DLL_FAIL";
	case NTE_PROV_DLL_NOT_FOUND:
		return "NTE_PROV_DLL_NOT_FOUND";
	case NTE_BAD_KEYSET_PARAM:
		return "NTE_BAD_KEYSET_PARAM";
	case NTE_FAIL:
		return "NTE_FAIL";
	case NTE_SYS_ERR:
		return "NTE_SYS_ERR";
	case NTE_SILENT_CONTEXT:
		return "NTE_SILENT_CONTEXT";
	case NTE_TOKEN_KEYSET_STORAGE_FULL:
		return "NTE_TOKEN_KEYSET_STORAGE_FULL";
	case NTE_TEMPORARY_PROFILE:
		return "NTE_TEMPORARY_PROFILE";
	case NTE_FIXEDPARAMETER:
		return "NTE_FIXEDPARAMETER";
	case NTE_INVALID_HANDLE:
		return "NTE_INVALID_HANDLE";
	case NTE_INVALID_PARAMETER:
		return "NTE_INVALID_PARAMETER";
	case NTE_BUFFER_TOO_SMALL:
		return "NTE_BUFFER_TOO_SMALL";
	case NTE_NOT_SUPPORTED:
		return "NTE_NOT_SUPPORTED";
	case NTE_NO_MORE_ITEMS:
		return "NTE_NO_MORE_ITEMS";		
	case NTE_BUFFERS_OVERLAP:
		return "NTE_BUFFERS_OVERLAP";
	case NTE_DECRYPTION_FAILURE:
		return "NTE_DECRYPTION_FAILURE";
	case NTE_INTERNAL_ERROR:
		return "NTE_INTERNAL_ERROR";
	case NTE_UI_REQUIRED:
		return "NTE_UI_REQUIRED";
	case NTE_HMAC_NOT_SUPPORTED:
		return "NTE_HMAC_NOT_SUPPORTED";		 
	default: ;
	}

	return "Unknown";
}	

// Added by ActiveEon 2011

// The key filename
static const char filename[] = "restrict.dat";

void wstring_to_bytearray(const std::wstring &src, unsigned char* &dst, int& dstSize)
{
	int length = src.length();
	// make sure the input string has an even digit numbers
	//if(length%2 == 1)
	//{
	//	src = "0" + src;
	//	length++;
	//}

	// allocate memory for the output array
	dst = new unsigned char[length/2];
	dstSize = length/2;

	std::wstringstream sstr(src);
	for(int i=0; i < dstSize; i++)
	{
		wchar_t ch1, ch2;
		sstr >> ch1 >> ch2;
		int dig1, dig2;
		if(isdigit(ch1)) dig1 = ch1 - '0';
		else if(ch1>='A' && ch1<='F') dig1 = ch1 - 'A' + 10;
		else if(ch1>='a' && ch1<='f') dig1 = ch1 - 'a' + 10;
		if(isdigit(ch2)) dig2 = ch2 - '0';
		else if(ch2>='A' && ch2<='F') dig2 = ch2 - 'A' + 10;
		else if(ch2>='a' && ch2<='f') dig2 = ch2 - 'a' + 10;
		dst[i] = dig1*16 + dig2;
	}
}

void bytearray_to_wstring(std::wstring &dst, const unsigned char *src, const int srcLen)
{
	std::wostringstream os;
	for (int i = 0; i < srcLen; ++i) 
	{	
		os.width(2);
		os.fill('0');
		os << std::hex << (int)src[i];

	}
	dst.assign(os.str());
}

DllExport int encryptDataStd(const std::wstring &inputData, std::wstring &outputDataInHex)
{
	cout << "Encrypt data with WinAES" << endl;
	int status = 0;	
	WinAES aes;

	// If no keyfile then the generated key is written into the file
	byte key[ WinAES::KEYSIZE_128 ];
	byte iv[ WinAES::BLOCKSIZE ] = {0x49,0x91,0x6E,0xDC,0x49,0x91,0x6E,0xDC,0x49,0x91,0x6E,0xDC,0x49,0x91,0x6E,0xDC};	

	// Open for read
	fstream keyFile(filename, std::ios::in | std::ios::binary);	
	// If the file exist Check for the key file 
	if (keyFile.good()) {
		// If file exist read it and load the key
		keyFile.read((char *)key, sizeof(key));
	} else {
		// Open for write
		keyFile.open(filename, std::ios::out | std::ios::binary);

		// If cannot open for write exit
		if (!keyFile.is_open()) {
			cout << "Unable to open the keyfile" << endl;
			status = 2;
		}
		// If not exist generate a new key and store it
		aes.GenerateRandom(key, sizeof(key));
		keyFile.write((const char*)&key, sizeof(key));
	}

	keyFile.close();
	
	const wchar_t *plaintext = inputData.c_str();	
	byte *ciphertext = NULL;

	try
	{
		// Set the key and IV
		aes.SetKeyWithIv( key, sizeof(key), iv, sizeof(iv) );
		size_t psize=0, csize=0;

		// The space in bytes required to store the input and terminating null character
		psize = (inputData.length() + 1) * sizeof(wchar_t);

		if( aes.MaxCipherTextSize( psize, csize ) ) {
			ciphertext = new byte[ csize ];
		}

		// Encrypt data
		if( !aes.Encrypt( (byte*)plaintext, psize, ciphertext, csize ) ) {
			cerr << "FATAL: Failed to encrypt plain text, the ProActive Agent will not work." << endl;
		}						

		// Convert encrypted data to hex string			
		bytearray_to_wstring(outputDataInHex, ciphertext, csize);			
	}
	catch( const WinAESException& e )
	{
		cerr << "Exception: " << e.what() << endl;
		status = 1;
	}

	if( NULL != ciphertext ) {
		delete[] ciphertext;
		ciphertext = NULL;
	}	

	return status;
}

DllExport int decryptDataStd(const std::wstring &inputDataInHex, std::wstring &outputData)
{
	int status = 0;
	WinAES aes;		

	byte key[ WinAES::KEYSIZE_128 ];
	byte iv[ WinAES::BLOCKSIZE ] = {0x49,0x91,0x6E,0xDC,0x49,0x91,0x6E,0xDC,0x49,0x91,0x6E,0xDC,0x49,0x91,0x6E,0xDC};

	// Read the key from file
	fstream keyFile(filename, std::ios::in | std::ios::binary);	
	// Check for the key file 
	if (keyFile.good()) {
		// If file exist read it and load the key
		keyFile.read((char *)key, sizeof(key));
	} else {
		cout << "Unable to open the keyfile" << endl;
		status = 2;
	}

	keyFile.close();

	byte *ciphertext = NULL, *recovered = NULL;

	int csize;
	wstring_to_bytearray(inputDataInHex, ciphertext, csize);		
	try
	{
		// Set the key and IV
		aes.SetKeyWithIv( key, sizeof(key), iv, sizeof(iv) );

		size_t rsize=0;

		if( aes.MaxPlainTextSize( csize, rsize ) ) {
			recovered = new byte[ rsize ];
		}

		if( !aes.Decrypt( ciphertext, csize, recovered, rsize ) ) {
			cerr << "Failed to decrypt cipher text" << endl;
			status = 3;
		}			

		int len = (rsize / sizeof(wchar_t));

		// Copy the decrypted data (recovered is formed by the null-terminated character sequence)
		outputData.assign((wchar_t*)recovered, len);
	}
	catch( const WinAESException& e )
	{
		cerr << "Exception: " << e.what() << endl;
		status = 1;
	}

	if( NULL != ciphertext ) {
		delete[] ciphertext;
		ciphertext = NULL;
	}

	if( NULL != recovered ) {
		delete[] recovered;
		recovered = NULL;
	}

	return status;
}

extern "C" {

	DllExport int encryptData(const wchar_t *inputData, wchar_t *outputDataInHex){
		std::wstring input(inputData);
		std::wstring output;

		// Encrypt input data
		const int res = encryptDataStd(input, output);

		// Copy encrypted output into a cstring
		wcscpy_s(outputDataInHex, output.length()+1, output.c_str());

		return res;
	}

	DllExport int encryptDataWithLog(const wchar_t *inputData, wchar_t *outputDataInHex, wchar_t *logFile){
		std::wstring logFilePath(logFile);
		std::string logFilePathStr(logFilePath.begin(), logFilePath.end());

		// save old buffers
		std::streambuf *coutbuf = std::cout.rdbuf(); 
		std::streambuf *cerrbuf = std::cerr.rdbuf();
		std::wstreambuf *wcoutbuf = std::wcout.rdbuf();
		std::wstreambuf *wcerrbuf = std::wcerr.rdbuf();
		std::ofstream myfile;
		std::wofstream myfileW;
		int res;
		try
		{
			myfile.open(logFilePathStr, std::ios_base::app);
			if (myfile.is_open()) {
			
				//redirection cout, cerr, streams to file
				std::cout.rdbuf(myfile.rdbuf()); //redirect std::cout to logFile
				std::cerr.rdbuf(myfile.rdbuf()); //redirect std::cerr to logFile				
			}
			else cerr << "Unable to open log file: " + logFilePathStr << endl;

			myfileW.open(logFilePathStr, std::ios_base::app);
			if (myfileW.is_open()) {

				//redirection wcout, wcerr streams to file
				std::wcout.rdbuf(myfileW.rdbuf()); //redirect std::wcout to logFile
				std::wcerr.rdbuf(myfileW.rdbuf()); //redirect std::wcerr to logFile				
			}
			else cerr << "Unable to open log file: " + logFilePathStr << endl;
			
			// Encrypt input data
			res = encryptData(inputData, outputDataInHex);
			
			std::cout.rdbuf(coutbuf); //reset to standard output again
			std::cerr.rdbuf(cerrbuf); //reset to standard error output again
			std::wcout.rdbuf(wcoutbuf); //reset to standard output again
			std::wcerr.rdbuf(wcerrbuf); //reset to standard error output again
		}
		catch(std::ofstream::failure &writeErr)
		{
			myfile << "Error while writing to log file: " << writeErr.what() << endl;
		}
		myfile.close();
		myfileW.close();

		return res;
	}

	DllExport int decryptData(const wchar_t *inputDataInHex, wchar_t *outputData){
		std::wstring input(inputDataInHex);
		std::wstring output;

		// Decrypt data
		const int res = decryptDataStd(input, output);		

		// Copy the decrypted output into the c string
		wcscpy_s(outputData, output.length()+1, output.c_str());

		return res;
	}	
}