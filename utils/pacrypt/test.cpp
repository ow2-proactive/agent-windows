// test.cpp
//

#include "stdafx.h"
#include "WinAES.h"

#include <iostream>
using std::cout;
using std::cerr;
using std::endl;

int main(int /*argc*/, char* /*argv[]*/)
{
    WinAES aes;
    // WinAES aes( NULL, WinAES::THROW_EXCEPTION );

    byte key[ WinAES::KEYSIZE_128 ];
    byte iv[ WinAES::BLOCKSIZE ];

    aes.GenerateRandom( key, sizeof(key) );
    aes.GenerateRandom( iv, sizeof(iv) );

    char plaintext[] = "Microsoft AES Cryptographic Service Provider test";
    byte *ciphertext = NULL, *recovered = NULL;

    try
    {
        // Oops - no iv
        // aes.SetKey( key, sizeof(key) );

        // Oops - no key
        // aes.SetIv( iv, sizeof(iv) );

        // Set the key and IV
        aes.SetKeyWithIv( key, sizeof(key), iv, sizeof(iv) );

        // Done with key material - Microsoft owns it now...
        ZeroMemory( key, sizeof(key) );

        size_t psize=0, csize=0, rsize=0;

        psize = strlen( plaintext ) + 1;
        if( aes.MaxCipherTextSize( psize, csize ) ) {
            ciphertext = new byte[ csize ];
        }

        if( !aes.Encrypt( (byte*)plaintext, psize, ciphertext, csize ) ) {
            cerr << "Failed to encrypt plain text" << endl;
        }

        if( aes.MaxPlainTextSize( csize, rsize ) ) {
            recovered = new byte[ rsize ];
        }

        // re-syncronize under the key - ok
        // aes.SetIv( iv, sizeof(iv) );

        if( !aes.Decrypt( ciphertext, csize, recovered, rsize ) ) {
            cerr << "Failed to decrypt cipher text" << endl;
        }

        if( psize == rsize && 
            0 == memcmp( plaintext, recovered, min(psize,rsize) ) )
        {
            cout << "Recovered plain text" << endl;
        }
        else
        {
            cerr << "Failed to recover plain text" << endl;
        }
    }
    catch( const WinAESException& e )
    {
        cerr << "Exception: " << e.what() << endl;
    }

    if( NULL != ciphertext ) {
        delete[] ciphertext;
        ciphertext = NULL;
    }

    if( NULL != recovered ) {
        delete[] recovered;
        recovered = NULL;
    }

	return 0;
}
