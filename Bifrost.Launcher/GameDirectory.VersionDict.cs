﻿namespace Bifrost.Launcher
{
    public partial class GameDirectory
    {
        private const string ExecutableNameVanilla = "MarvelGame.exe";          // Original name
        private const string ExecutableName2015 = "MarvelHeroes2015.exe";
        private const string ExecutableName2016 = "MarvelHeroes2016.exe";
        private const string ExecutableNameOmega = "MarvelHeroesOmega.exe";     // From 1.52.0.1168 (2017-07-05)

        private static readonly byte[] ShippingSignature = Convert.FromHexString("5368697070696E675C57696E33325C"); // Shipping\Win32\

        // SHA1 hashes of Win32 executables for detecting version
        private static readonly Dictionary<string, string> VersionDict = new()
        {
            // 1.9
            { "942464121E433F5FFF0389B6015B296F8411D341", "1.9.0.614" },
            { "82F8D87C2E780A3491B4EC49575B2542DE2F0375", "1.9.0.645" },
            // 1.10
            { "3E70AE41398D44836BFFE1971B728DF67E35568C", "1.0.2447.0" },
            { "0961DE7BA206421D087D45D0521E291F4B885389", "1.0.2470.0" },
            { "6467E9AF671796C9ECBBB568A9CB8B707D0B8DFD", "1.10.1.1" },
            { "4D394193827D09B1A2F0CC93143998FBFC835FD5", "1.10.0.45" },
            { "201504471BEDF04B263EF64439D86981FAE4B322", "1.10.0.66" },
            { "4A4B6E6EF14B9F5F5D3DD193CA6ED4625F62E61C", "1.10.0.69" },
            { "9D3C32A42EB7455C042D8C7A2BCE2C7607FF1965", "1.10.0.68" },
            { "F1C99B3DF9B57B5490F7E124D9183B6F90BA491D", "1.10.0.83" },
            { "0BC2C93BA0D4075D2B3A4B407D6B920FB553BA05", "1.10.0.106" },
            { "DFDD719C7F389305C362115DDE7B0C501D56A3DB", "1.10.1.4" },
            { "AA01514BFA14A01045C992A881296A9C3741A2B5", "1.10.1.8" },
            { "D4D5A76CBBAFD77E778151EC309D5C9DA190DC28", "1.10.1.14" },
            { "9E2D2692B145ACF852621A7EA49F718C1792310E", "1.10.0.186" },
            { "A5C77E84C1F83A32214DA89E44B4FBA3B4B7ACF2", "1.10.0.253" },
            { "840E647385B6DE0BC2A3B41FD942EF019F73AD27", "1.10.0.299" },
            { "8923A10DD299EB72FFA144EEF64926BF4C92E0B7", "1.10.1.47" },
            { "70C4B0DF77DA3FC272D9FDD5529D9284A34ECB01", "1.10.0.469" },
            { "D06EB18F3CF319526DE5F58E0FC33E717D15CCC3", "1.10.0.609" },
            { "26F20D918039C6214049668FA9ADFA35B9689E9E", "1.10.0.623" },
            { "B93109BC35A326C842009892FC6084E3DEC35F28", "1.10.0.643" },
            // Unsorted
            { "DF59D785BEFC368FD8A40BD135261E82C62AECCF", "1.0.3753.0" },
            { "CB5F1E1688CBA0DD2688D4EFD61615D0D890B411", "1.41.0.533" },
            // 1.45
            { "950841B8B5F7DC13642353BB47FBB44178D9D6DC", "1.0.5664.0" },
            { "9EA9D4C111667C981DEC6A9D0193A5E621E6FB93", "1.0.5667.0" },
            { "CCB7AA792518F3F7AE2BCA7A63F066F2BBD3A939", "1.45.0.25" },
            { "8E4E183FAE62FA2F17E7B6B57096CB9088DDD9D9", "1.45.0.38" },
            { "592FE6016447EB4D3E51AFDB46172BB2483AF49B", "1.45.0.70" },
            { "718E15D17742CA1584EAE6FDCC9BE07D82088F30", "1.45.0.88" },
            { "10A9DFC2A38A4186AA686AF60E84C007C40F561A", "1.45.0.98" },
            { "26662CF04EBF33203EA5BAA6FB504C105C51F503", "1.45.0.107" },
            { "1F9E152F4E165B51C0FB84DFF48A86EC91DC0CEB", "1.45.0.114" },
            { "E442AEE024BF107A1AE2EAF12C91F301BAF17EBE", "1.45.0.123" },
            { "884F81183EA3BE32BFA6926D0C71C20441A2854E", "1.45.0.178" },
            { "92B4ABDEAD6B24812C691B6CCACF2AB0FF73A0B9", "1.45.0.195" },
            { "A0C1652C6C555A14B959879D377290ADD2E83000", "1.45.0.203" },
            { "1D611CC1D91C9956E0D06EA1C32E5E3DCD0A70E9", "1.45.0.226" },
            { "B8625A2E6932934733BEE29DEB89D68F561159CD", "1.45.0.236" },
            { "2D1A71A9EA21BF6A656E8E35825DC780B20B7B45", "1.45.0.260" },
            { "C882C80059B189F0C42FAE59D9FDB795DC6D9E05", "1.45.0.294" },
            { "1B221C730B3900E98F467D7D00099933A4EA5F9D", "1.45.0.303" },
            { "2538F7BE395368AD4E971DAF22990CD18D53298F", "1.45.0.319" },
            { "9B397AB48CD7AA1EAC8FBFCB8E62C03F1DB5C193", "1.45.0.332" },
            { "5789F2922440618AE52D44C6C5D923F12AC07ECE", "1.45.0.384" },
            { "D914E585FFFF533766B510FB141C7A54720C21E9", "1.45.0.400" },
            { "E1843371C5E13D63D2FB9ADD9C8CA5E77AFC9CCF", "1.45.0.416" },
            { "783998746710022DCD47A6B1C06A12E3B9A9230A", "1.45.0.421" },
            { "8BA276013C388BA49175AE69A8D6D92C08C2F7FC", "1.45.0.438" },
            { "0D9BD705749D843C01654E59E0BE94C93328783B", "1.45.0.486" },
            // 1.46
            { "97BF83DEA345C68A6B2AEE019C063DF655CDF25E", "1.0.5734.0" },
            { "42C09069678F430A692DF835C00A4541A40E482B", "1.46.0.15" },
            { "ED3D36F3B6D1ECA2E6E763E5DB8675190DCA2CD9", "1.46.0.40" },
            { "E26B77372B2E5CD6F157BC6F68CCFA35B586CBF8", "1.46.0.54" },
            { "F83721219AC13A26E3A887DFC9ACE7267607DC14", "1.46.0.67" },
            { "E0A9CC98D638B81971EF5D9029D0DD15E844B333", "1.46.0.109" },
            { "D9CD7F189395F1202F32D552729603A0EFD1F025", "1.46.0.138" },
            { "64B4C5AD756EF6F0B1AD97EFBDDE60409AEEBFF3", "1.46.0.151" },
            { "82C579EA0CBC7501101CB139AAB7AA54E3C4800F", "1.46.0.157" },
            { "853D0DD64D54446B9B8A795069455829826DBB4A", "1.46.0.171" },
            { "8C6C9735315E346F904B89E35B53EDBF160A64A5", "1.46.0.209" },
            { "634E61E2268DEF43B6B22FFAF03B5F7C21FB35AA", "1.46.0.220" },
            { "3C5D604FC7097BB4B3AB95250B3E2B58171DCD4B", "1.46.0.233" },
            { "7F7738B7B7B82FABB5FF72F57EAC3E6C46077AF1", "1.46.0.244" },
            // 1.47
            { "54CE17F630310F1526E8AECF94C316BC44A0673A", "1.47.0.24" },
            { "584CDEBD4D45894E1714F4941AB58553053D476A", "1.47.0.40" },
            { "2EB697ECADA1446B7168CDACF27F5C7F55B65E53", "1.47.0.56" },
            { "F60548E079C5F66D61D8B2B493BA2EFBC89A3B55", "1.47.0.71" },
            { "A95D100D60A0DB901C61C4755B9CF010202712A8", "1.47.0.86" },
            { "241781CA39B56059AE6820BFE720DFFC177E4156", "1.47.0.101" },
            { "AC4C5C5264BF4FB772CDF58C28239F26CE632100", "1.47.0.124" },
            { "E5A368B68D03DCE9052823C903647D8CE58E0E78", "1.47.0.132" },
            { "E4E5D3B1AEBE088BC0B87F3AB41CF50840779795", "1.47.0.146" },
            { "E556B42502AF0CAC0008B034E7274CF277E43C02", "1.47.0.160" },
            { "A0BC31C118E4FAF3AD09F7B4F3D138A1D95742B1", "1.47.0.169" },
            { "DF9659E058ED291AB6077C110ACEA71F06D99BAD", "1.47.0.180" },
            { "06F61A32A3E44ACC228D40E9AD31BB12FDA189B7", "1.47.0.196" },
            { "9FDDAAA365D94964BAAB536240941A7616C57DC7", "1.47.0.244" },
            { "ACF6B059C6A3809C8637C0E0A2E30003E89104DF", "1.47.0.261" },
            { "B290EA25FF440108484A8FCEDFA6BD92990B5034", "1.47.0.269" },
            { "409B8F4E80925528901EA58997A388D37CE43A9C", "1.0.5851.0" },
            { "06989C0C6B494885FE03FD871846DB0F955DDBD5", "1.47.0.372" },
            { "FDDCCC49977BF64FE8C6E336F694BDDC5B7DB27C", "1.47.0.378" },
            { "0E786AE2C6263AD41DE10D8048401DA28EFD1863", "1.0.5868.0" },
            { "51DB2229CD0DA024584953F3C57524E610BD34B1", "1.47.0.466" },
            { "30C800667DCCBB300E5E79CFAB08FBD80A3A4C04", "1.47.0.482" },
            // 1.48
            { "1F12144FF31A7DC6202938D28BA3F27ED77F9821", "1.48.0.21" },
            { "23EFD3FE1016A84BCACAE6AE2DCC3D7880187A0F", "1.48.0.36" },
            { "D3D2DDA2C4DC5F18F5CA53086C9D0AFB0047F90A", "1.48.0.51" },
            { "D18AE1E1866E9400AC94C6AC2D98DF3C461AC6F3", "1.48.0.66" },
            { "45CD9020B0FBA37FC9EB7D12DBAAEB74AE9E02EA", "1.48.0.114" },
            { "04F5EA7F45AD9BE93C33B11E1C7163D524BCCB90", "1.48.0.129" },
            { "B1A2B96BECACDC514704191493B7594C9A52DBC6", "1.48.0.145" },
            { "9913B4C62E724237CBDE649A4B93B1A4A7719394", "1.48.0.158" },
            { "B20897F80184541352085FD750D341BB822CF62B", "1.48.0.173" },
            { "1219C64EDD40A5AD94CCBF06E3DFD479D72C9F2A", "1.48.0.220" },
            { "0E385163148B2F7EC8F64BC73ABD9A348647347A", "1.48.0.250" },
            { "023EF5F39E7BB1F11C7F75603BFB35D3B1264AB0", "1.48.0.255" },
            { "EE38805B924F73862D2D9B4B325D6AA4FC0AEE49", "1.48.0.282" },
            { "A7802E5FAF02D37C18DFBD4FFF8B6BDC96F1A159", "1.48.0.329" },
            { "D762CCAED06D8932CEA03BD275714E2127BD4344", "1.48.0.346" },
            { "3B6E7D7194BE196BA1D50437E6171944B9148AEB", "1.48.0.365" },
            { "320F6B902516B2EDDC01371787D9B3CDA8844F4C", "1.48.0.384" },
            { "4FECD6E2341945091382851186332CDAB09BE28C", "1.48.0.392" },
            { "634D226D511D0F49A009F2DF74436FAF27543486", "1.48.0.406" },
            { "93B8B047945F5B2D81AD931CADF637D1BA92165A", "1.48.0.453" },
            { "B60D3CFA0AD29717F5517FB44EA23559E423E390", "1.48.0.468" },
            { "3AEF07F223AA819A0AC05C64B7ACADBC23922954", "1.48.0.484" },
            { "9D347505FBC74FA8744F9B2D02200DF9F7D8301D", "1.48.0.493" },
            { "C9211273C3CBAEF758F3D7913477C991A16AB57D", "1.48.0.518" },
            { "DC8D975B7CD782D802F1E8DAB54E2BE79464F358", "1.48.0.565" },
            { "5B3B3442954D85E67611E398A557C53F53CF4ADE", "1.48.0.585" },
            { "DC570DAEDE6F1C5D1D98DDBC721B7B0ECAB84080", "1.48.0.611" },
            { "33D3CC85438588E381610A1C489390B3C178142B", "1.48.0.657" },
            { "BBC031F855BCC2D9EC47013783B8ACB554253C48", "1.48.0.662" },
            { "CE3E115A1FCEBE76767B0CAAF0513CD60170F83C", "1.48.0.709" },
            { "0F25019239DBF8E6606AEF4513B5B43566737C93", "1.48.0.758" },
            { "7C2B7D1BAC4B1BC81BEE05ED3F4FEBBA93B5A38B", "1.48.0.771" },
            { "68AEAFC70D685D738952532CFB991D13B87D8CF6", "1.48.0.774" },
            { "5E4139F8BBCF0114E3584EB97F2D42B02CF180C2", "1.48.0.790" },
            { "1D3A5CBE695ACB27F7C0E8CEEBE199BBD6B8CC1D", "1.48.0.840" },
            { "6DD23F26B34BAF6CD31817D845ABFEBE66F3F3F4", "1.48.0.877" },
            { "40CB91B131C6C05B8E9F0FBA9E2BDA46061EA2BC", "1.48.0.881" },
            { "A69F602B7DD6F431FD6D4464E1BB0F7A8301CB06", "1.48.0.890" },
            { "FB109BAFBFAE21732011BFB339177731B14A657F", "1.48.0.902" },
            { "5E1D2C43FEE47140486F984A090F9F045ED75243", "1.48.0.932" },
            { "BB8B53388C296DECE2CC4739DF0373C62CA2B91D", "1.48.0.939" },
            { "85FF8021921D55062DA4566AA54DB9FC74EC39C2", "1.48.0.946" },
            { "C2732B75E26255FC80B7EB292B22DA063CAB7254", "1.48.0.1033" },
            { "24A55C9EF1E5DB9C9AA129C4D9FA9F1F4A117D5F", "1.48.0.1050" },
            { "1B05BDC1AEEF31BB9B242341B5C6649F067845C1", "1.48.0.1059" },
            { "2FAD56A61B3AB668D5DB1799B2A9B50E5C0EA908", "1.48.0.1069" },
            { "FF5E96F4C87BCEADCB67E97DA0A3AE74A5BE5F76", "1.48.0.1078" },
            { "2054D34D0DCF64E0E21B82CC865410E2332C4916", "1.48.0.1114" },
            { "1D6DF5280A5BAD2DEAFDD84022679869B0A49AE6", "1.48.0.1126" },
            { "183ADD04813BF78C3D542CF9AB5ECCEB941BEE92", "1.48.0.1137" },
            { "064F006AF758AB5F9876324946774389F54E9E28", "1.48.0.1213" },
            { "36B852B9656F3BEB9A3BCE74C6F588873EAC2133", "1.48.0.1227" },
            { "146FC844DC7EBF16B9F5813117E77ECAB1DD0FA7", "1.48.0.1400" },
            { "424DA5907CCE073E757B0B680216A0EE5CFA7A58", "1.48.0.1425" },
            { "C2567FF733772D45AE5C8B1662EEE06F03F36DC6", "1.48.0.1447" },
            { "C7EC583A82C893CC332D98560828379DD02F8539", "1.48.0.1487" },
            { "E6AAF5848265BC2AD768931997E881B14C264134", "1.48.0.1495" },
            { "66F51E06C344CF102CE2FC11FA4BD3DDC3EA8630", "1.48.0.1502" },
            { "2AFC491BEA2F61D193959583329B9701EF7182D9", "1.48.0.1563" },
            { "362693D06978690601F335C21AE98A3DB14267BF", "1.48.0.1618" },
            { "CF4F8AFCD7C6198D2D6DAD17BE97289D71C16826", "1.48.0.1668" },
            { "D95D7F016BA986085A7C4760F411DE30A06C9572", "1.48.0.1712" },
            { "9668354A10B573EA063B35CAF2009CE8F70A885E", "1.48.0.1891" },
            // 1.49
            { "03BB5A4B38A16AF038A64C7A9B5D3F959CA9A409", "1.0.6213.0" },
            { "2CF51BA98C4BA9013EF7A20BF716CCC1FC1321FD", "1.49.0.22" },
            { "F96019EE1F3C6CFD39DF53ACC2E1CA866C716CC9", "1.49.0.112" },
            { "90460356A999E3D43E73FE25EE0CD558C97ED822", "1.49.0.181" },
            { "30B7C7304828FCFE8DB7D01781390F2CD8C85112", "1.49.0.191" },
            { "D0E970C6F1AEBFC6084B8C5B56D4BF95E8C365D6", "1.49.0.213" },
            { "39501A5119FF66CB874ACBCBBB72E9EDD8A577F6", "1.49.0.255" },
            { "E76F957A992B907C27DD85204E61D259427E532A", "1.49.0.285" },
            { "E35CAFA416E4539339A4540B4E485F30940145B1", "1.49.0.341" },
            // 1.50
            { "8BD9257F1F6E017965ED074852753F73DA92C287", "1.0.6308.0" },
            { "B2C8F67E6FEB598F9A5E006F50DEF590476CB955", "1.0.6311.0" },
            { "51A766CAA974FD886F98D48F2518240740002987", "1.0.6316.0" },
            { "F7D98C1271E694F0638601E779389B46FC38849A", "1.50.0.28" },
            { "F2C72665485E3A93B49486F1E79A81EBDD42BE20", "1.50.0.38" },
            { "DCD7CFD6E3BAC9E5448697B2316FEF7D883DD1C6", "1.50.0.63" },
            { "1797EE5E71D93FAACA053EF5C29D65421221132F", "1.50.0.90" },
            { "1CD93FACBFF7BB91D3615AAE695EC53202C88DA7", "1.50.0.102" },
            { "1DB20C1F7119FD1E0E59D8FE05F5772E34CC9E34", "1.50.0.115" },
            { "63555A76FF98765D8A0FAD9F8EDD4D31A1911D02", "1.50.0.125" },
            { "1D6A787EAF25CA1FCE5C90F62C0CCE317A48593C", "1.50.1.1" },
            { "7827A0A1D534C34E4C815C76B6AA13BD6136F5AC", "1.50.0.177" },
            { "AB11437DE3951173D0B27F7710BAECE518A12B64", "1.50.0.188" },
            { "A174CF11433253B6847399D60CAB4B64023966D1", "1.50.0.202" },
            { "2BE0B7D1D09A2ACFC17D4821B6ABBE35F50BF474", "1.50.0.205" },
            { "1F6383387CFFF781F18707F4A2A7F9CF7ED6D4B5", "1.50.0.225" },
            { "ED0BC355CBE03DE259D178F0C751D62BE0043F3C", "1.50.0.237" },
            { "A6237ABA2BA25E1489DB17FB5265BCB0C1A33419", "1.50.0.267" },
            { "A79FE463EC89093484C31CD90DFF0761F17F5398", "1.50.0.283" },
            { "0632546DE118CFD311A67A94B103D02E7E3A4150", "1.50.0.298" },
            { "2D37E6713E772554C59AB525A6C24C28A61EB408", "1.50.0.344" },
            { "7CF416E6B6915DE8C2D3075F373BA645D9F2B7C9", "1.50.0.369" },
            { "E67F6460818BD3955FD320D6785C3AFEB3FFB801", "1.50.0.380" },
            { "95A7AA4248A2C0D246273D04786C7D62C2B1141D", "1.50.0.392" },
            { "908F6E2B082D4B4395A3E5D99B73AE27A3E8ED41", "1.50.0.402" },
            // 1.51
            { "DB803977873F45CF9310746517A5698ABB6E05F9", "1.51.0.8" },
            { "186A0F444F4F5FD820B08619EAF637680C85C8DE", "1.51.0.43" },
            { "496E5DFE9656201F394C3F3BB085338C2C583CB4", "1.51.0.64" },
            { "AF25FB61C2543520D5B0C2096170B8938C43525D", "1.51.0.100" },
            { "A4B280DC4C6E1782FEB33F7573C7C9120F7DFAB2", "1.51.0.111" },
            { "34E7ED7A9927749D590958E8F41651F816587D94", "1.51.0.173" },
            { "2FD04C4D57A301B743F6C57A02BA0C5D13B25D7C", "1.51.0.182" },
            { "895C369BD12CD92B1A8E68585843301575CDD238", "1.51.0.245" },
            { "90141D8CF53080C03766C244364F6F4536F79D1E", "1.51.0.254" },
            { "8AF0EEB3009B6283A68EEAB730D8D57DC925052B", "1.51.0.268" },
            { "537F483A93F1E5EAA1162132DBDDA25A06BF8AEE", "1.51.0.297" },
            { "B90BDF4C5AAFACDD4F1C56682B8D700ECE55D8A4", "1.51.0.310" },
            { "28CC923B8B511A11C9B61473886128411ABF6A21", "1.51.0.317" },
            // 1.52
            { "8A3125C1CA9579696C2F21BDB053AB4C33242681", "1.52.0.116" },
            { "907F4BD53A31F282E770E4F87AACD71625686AD8", "1.52.0.140" },
            { "3E7D6868603F268F83A9DD728FA07C8AC34F0453", "1.52.0.199" },
            { "586EAC821FB20B7D24D9A6218406859E3B14AF07", "1.52.0.209" },
            { "C277DAD89278735A3A316114D0B470D5DE21C403", "1.52.0.226" },
            { "D7772BEC1778B2E73EBD45FE8F6AF479AABA27C3", "1.52.0.236" },
            { "054B7B4311F5296C8A7AB9FFDD37452FE5E2A97E", "1.52.0.239" },
            { "28F39A8F6935A19BFE199ACA0F2B34D3DCA17CF2", "1.52.0.256" },
            { "E760F747E0D739D6E965FE204A69E6BF45A142F7", "1.52.0.293" },
            { "BD26318F52FE6CD77209B8F7400A6801FD7316C2", "1.52.0.311" },
            { "573358BE6910E9E3F19FEAD27ACA200DF5EB285C", "1.52.0.320" },
            { "517D5803E1EB065FB580A25573A1C6E020B7F2A6", "1.52.0.354" },
            { "531AD83C8CF9AC72F1A896442C1D424DD76BDBA2", "1.52.0.396" },
            { "E8FB6F598AA2E9D8FF0DFB2E96E0EB4C4B5B5BB9", "1.52.0.433" },
            { "0409EB39BB7847D40DC8CE6BACC4D20BA4F85BA5", "1.52.0.519" },
            { "892E3A5875591BC52AF7CBCEA3BA9BAB1439CA5D", "1.52.0.529" },
            { "D5DC5D8A8FFCF017CE8A0187EE750F0A9CA4802A", "1.52.0.539" },
            { "F0F46668C0AA9FACD41979DCFF23394195A381F7", "1.52.0.576" },
            { "5C12DFFD7DE2B786C0F84F5EC4B40D1473955ECC", "1.52.0.585" },
            { "2153976355ADBF41CBB8835580449AE9A271D91D", "1.52.0.763" },
            { "93515AD1C8F342874EA0F41D5127429DEED044DA", "1.52.0.803" },
            { "E872910C81A702EA05E158E70270D75D81E27A94", "1.52.0.837" },
            { "F0B11414EEB3BF439AB807FFF118787BA4C43993", "1.52.0.845" },
            { "D7C78A7EC48BC982546B79A755BB2B900BFB50D9", "1.52.0.855" },
            { "295582D655E1FED85140EB9CD2243BD21BBE6EF5", "1.52.0.862" },
            { "7D9AD6FDB964617C0372D20039DB367A24387522", "1.52.0.906" },
            { "2B306767070B8A56952EBD65A9F2282A436DECAF", "1.52.0.913" },
            { "3543AFDD4BF965354313D0E39ED98329A462029B", "1.52.0.921" },
            { "7D9F9873DDC6FBC4AB362F3D40F98D1F5BFD1B12", "1.52.0.926" },
            { "11A6370FC2ED786B284DFF44765A430769B19D4A", "1.52.0.1051" },
            { "F0CFA09C3AB9E2A3FA8B45B6669F61E1747ED9A3", "1.52.0.1085" },
            { "A8CE6C7CF5BC77E8A58014F902FBACF56C5F8380", "1.52.0.1094" },
            { "03876BB5F0A6CDC83C578A7056662E6323D81DEA", "1.52.0.1101" },
            { "35B1551125922F27317ABEC2B1C709FF573339B5", "1.52.0.1168" },
            { "16C5BEE9C8D764CD13CB40D4EB70CC6692243D7A", "1.52.0.1175" },
            { "1BD05A36BB3B43263ECA28E0E265A5799EF6A5F3", "1.52.0.1228" },
            { "D7C24695D85587A18F94046785F0112B3D896353", "1.52.0.1237" },
            { "54051EE9898A8F87E83FEFDC6AA086272F89C32B", "1.52.0.1246" },
            { "DAAB7DCC2DA2D6C0D20A05387459E0BA528F2DA3", "1.52.0.1255" },
            { "33FF4E4EA42A0BF8A8AAE7A53B20EA673803E472", "1.52.0.1284" },
            { "40FA0564B2947704D13F7918C26C50C01D1A4657", "1.52.0.1318" },
            { "393FE67312D070E43575DEAE6A4B4A298399D38E", "1.52.0.1352" },
            { "3E09B5C8846D22EEEC5E4EEFA7CBE804057B12B5", "1.52.0.1335" },
            { "879F1689D98892290CA81A229451AE106F9029A9", "1.52.0.1503" },
            { "6F36D2A27A9D8FBD2A7EFB13625D5912D4F3357B", "1.52.0.1595" },
            { "65D34B6FA10419ACFE2EBA83278BFDA00C49E7C9", "1.52.0.1604" },
            { "A82EA749034863BF32A0727E144EEC84F789674E", "1.52.0.1610" },
            { "AABFC231A0BA96229BCAC1C931EAEA777B7470EC", "1.52.0.1700" },
            { "4F639DC4D6DED306E842E06082883E8405B5E964", "1.52.0.1927" },
            // 1.53
            { "D86696464DAFC7FA10F50A552FB09E930053129A", "1.0.7030.0" },
            { "BF6C7C40180E2C0090EE7C82872980EF58C46336", "1.0.7036.0" },
            { "3F5FA099E7680535D379325450E3DF1EA28907B7", "1.0.7040.0" },
            { "085DFB277CDC5455F84F5606440242BE022AA5F2", "1.0.7042.0" },
            { "3A2A7E2021DBDD2BB213D1AE1337538258BDA2A8", "1.0.7044.0" },
            { "A9B8C65C19E3664378BCC927F8A737D8E3D7065D", "1.53.0.15" },
            { "02F3D2BB66938F38627E90CB2097A4DA2DA5F469", "1.53.0.24" },
            { "226C986C02424F6447F8FCEE07B73633C64ECAC7", "1.53.0.33" },
            { "88ACFFEC982BB09347687F98BB65FA2C6D8D1006", "1.53.0.42" },
            { "594537C7EB3D1AE816C63671C6D62D75527A9609", "1.53.0.53" },
            { "EE8ED47BB75B59F3C9E45FAFFCC328CED7173846", "1.53.0.62" },
            { "84995A62E0AD798E02649171D6417CB670D73B8D", "1.53.0.73" },
            { "4C39A09877EAEFEC3A46E53B997FF5AF4CDACDEF", "1.53.0.82" },
            { "3C0319B5EE810D950026032AB8D010F051A04D33", "1.53.0.111" },
            { "88F604B6C584BA604536E2C873443840A2CC19B3", "1.53.0.123" },
            { "4283F7606A699398717510F5FDAFC762C9CBC4E7", "1.53.0.131" },
            { "FCF6218220DCCA2BEA699B1263DCAF975432359C", "1.53.0.142" },
            { "60C4190B2E57899EE1680DBC63EEB43EBD8EFD49", "1.53.0.153" },
            { "4449383D171F05AC30DB4E1E1EEB974E99E8DEF2", "1.53.0.184" },
            { "4F8FAECF475FB288356F544059C7DE01838F7B51", "1.53.0.192" },
            { "95028D10B652B657CE3BBA083CCEFB4FAC0E356E", "1.53.0.203" }
        };
    }
}
