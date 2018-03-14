# From http://nsis.sourceforge.net/User_Management_using_API_calls#Add_User_to_a_group
# Put  in <NSIS home>/Include

!include "LogicLib.nsh"

!macro GetServerName SERVER_NAME_OUT
  Push $R0
  Push $R1
  Push $R2
  System::Call 'kernel32.dll::GetComputerNameExW(i 4,w .r0,*i ${NSIS_MAX_STRLEN} r1)i.r2'
  ${If} $2 = 1
   StrCpy ${SERVER_NAME_OUT} "\\$0"
  ${Else}
   System::Call "kernel32.dll::GetComputerNameW(t .r0,*i ${NSIS_MAX_STRLEN} r1)i.r2"
   ${If} $2 = 1
    StrCpy ${SERVER_NAME_OUT} "\\$0"
   ${Else}
    StrCpy ${SERVER_NAME_OUT} ""
   ${EndIf}
  ${EndIf}
  Pop $R2
  Pop $R1
  Pop $R0
!macroend

!macro AddUserToGroup1 SERVER_NAME USERNAME GROUP_ID
  Push $R0
  Push $R1
  Push $R2
  Push $R3
  Push $R4
  Push $R5
  Push $R6
  Push $R7
  Push $R8
  Push $R9
  # Add a user to a group using the group's SID (from well-known SIDs)
  # Administrators S-1-5-32-544 (use 544)
  # Users S-1-5-32-545 (use 545)
  # Guests S-1-5-32-546 (use 546)
  # Power Users S-1-5-32-547 (use 547)
  # Get the user's SID
  System::Call '*(&w${NSIS_MAX_STRLEN})i.R8'
  System::Call 'advapi32::LookupAccountNameW(w "${SERVER_NAME}",w "${USERNAME}",i R8, \
*i ${NSIS_MAX_STRLEN}, w .R1, *i ${NSIS_MAX_STRLEN}, *i .r0)i .r1'
  System::Call 'advapi32::ConvertSidToStringSid(i R8,*t .R1)i .r0'
  # Get the group's SID
  System::Call '*(&i1 0,&i4 0,&i1 5)i.R0'
  System::Call 'advapi32::AllocateAndInitializeSid(i R0,i 2,i 32,i ${GROUP_ID},i 0, \
i 0,i 0,i 0,i 0,i 0,*i .r2)'
  System::Free $R0
  System::Call '*(&w${NSIS_MAX_STRLEN})i.R9'
  System::Call 'advapi32::LookupAccountSidW(i 0,i r2,i R9,*i ${NSIS_MAX_STRLEN},t .r3, \
*i ${NSIS_MAX_STRLEN},*i .r4)'
  System::Call 'advapi32::FreeSid(i r2)'
  # Add the user to the group
  System::Call 'netapi32::NetLocalGroupAddMembers(w "${SERVER_NAME}",i R9,i 0,*i R8,i 1)i .r0'
  System::Free $R8
  System::Free $R9
  Pop $R9
  Pop $R8
  Pop $R7
  Pop $R6
  Pop $R5
  Pop $R4
  Pop $R3
  Pop $R2
  Pop $R1
  Pop $R0
!macroend