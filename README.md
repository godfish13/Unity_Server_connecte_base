# Unity_Server_connecte_base
 Base project which Unity-Server connected using google.protocol

1. Write Protocol.Proto in Common/protoc-23.3-win64/bin/
2. execute Common/protoc-23.3-win64/bin/GenProto.bat
3. It will generate Protol.cs and will generate CientPacketManager.cs to Server, ServerPacketManaer.cs to Unity
4. Write PacketHandlers

!! this project has Unity - gitignore but doesn't ignore .csproj and .sln
