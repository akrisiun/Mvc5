// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("System.Net.Http.Formatting")]
[assembly: AssemblyDescription("")]
#if !NETFX_CORE // GuidAttibute is not supported in portable libraries
[assembly: Guid("7fa1ae84-36e2-46b6-812c-c985a8e65e9a")]
[assembly: InternalsVisibleTo("System.Net.Http.Formatting.Test, PublicKey=00240000048000009400000006020000002400005253413100040000010001001d5b85029a2d79382fbd3b4e2771d8a262fa569613e2f31d270f7ddff2301f40887db09a1a6ebd09f66eac441d9a6832f9ff2494ed523f1c36066bd3f55ae2a6f02f2a729b8ca3086a22a22690ecb161a86338ec638ccb938e8b233f515dc925553a3f0f504da0685335d119fb08b36f3dd0613b4e7cfec045ba982395822ade")]
[assembly: InternalsVisibleTo("System.Net.Http.Formatting.Test.Integration, PublicKey=00240000048000009400000006020000002400005253413100040000010001001d5b85029a2d79382fbd3b4e2771d8a262fa569613e2f31d270f7ddff2301f40887db09a1a6ebd09f66eac441d9a6832f9ff2494ed523f1c36066bd3f55ae2a6f02f2a729b8ca3086a22a22690ecb161a86338ec638ccb938e8b233f515dc925553a3f0f504da0685335d119fb08b36f3dd0613b4e7cfec045ba982395822ade")]
[assembly: AssemblyVersion("5.2.7.0")]
#else
[assembly: InternalsVisibleTo("System.Net.Http.Formatting.NetCore.Test, PublicKey=00240000048000009400000006020000002400005253413100040000010001001d5b85029a2d79382fbd3b4e2771d8a262fa569613e2f31d270f7ddff2301f40887db09a1a6ebd09f66eac441d9a6832f9ff2494ed523f1c36066bd3f55ae2a6f02f2a729b8ca3086a22a22690ecb161a86338ec638ccb938e8b233f515dc925553a3f0f504da0685335d119fb08b36f3dd0613b4e7cfec045ba982395822ade")]
#endif
