using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OptimizedRandom
{
    private static readonly Vector2[] unitCircles = new Vector2[]
    {
		new Vector2(0.6284175f, -0.1594367f),
		new Vector2(-0.3520948f, 0.5094606f),
		new Vector2(-0.2197358f, -0.7768309f),
		new Vector2(0.1625242f, -0.1324621f),
		new Vector2(0.8325589f, -0.3162371f),
		new Vector2(-0.7967795f, 0.3167507f),
		new Vector2(-0.6874081f, -0.5632755f),
		new Vector2(-0.7793092f, 0.17913f),
		new Vector2(-0.874374f, 0.04271268f),
		new Vector2(-0.5674739f, -0.5545043f),
		new Vector2(-0.05020416f, -0.572431f),
		new Vector2(0.6999133f, 0.1529321f),
		new Vector2(0.1713488f, -0.4566568f),
		new Vector2(-0.472232f, -0.7578469f),
		new Vector2(0.02847173f, 0.05722259f),
		new Vector2(0.4278271f, -0.01292893f),
		new Vector2(-0.2002517f, 0.7441574f),
		new Vector2(0.052618f, 0.4370975f),
		new Vector2(0.3260661f, 0.5505584f),
		new Vector2(0.2137816f, 0.2908468f),
		new Vector2(-0.5025113f, 0.6196498f),
		new Vector2(0.9438601f, 0.1343812f),
		new Vector2(-0.5624834f, -0.04398106f),
		new Vector2(-0.6331355f, 0.5103567f),
		new Vector2(-0.6631063f, 0.4260402f),
		new Vector2(0.8259042f, -0.4172785f),
		new Vector2(-0.1793794f, -0.4789261f),
		new Vector2(-0.1947064f, 0.2967431f),
		new Vector2(0.8591769f, 0.4783701f),
		new Vector2(0.2727011f, 0.815209f),
		new Vector2(-0.7880667f, 0.08556416f),
		new Vector2(0.7259823f, -0.2473455f),
		new Vector2(-0.3036385f, -0.7023347f),
		new Vector2(0.1877773f, -0.2709677f),
		new Vector2(-0.5317625f, -0.4561744f),
		new Vector2(-0.5708732f, 0.006272819f),
		new Vector2(-0.5441311f, -0.523525f),
		new Vector2(0.712783f, -0.2856933f),
		new Vector2(0.5778471f, 0.6859649f),
		new Vector2(0.2338699f, 0.4176029f),
		new Vector2(0.200652f, -0.333095f),
		new Vector2(-0.1403714f, -0.3488768f),
		new Vector2(-0.2646534f, -0.9591296f),
		new Vector2(-0.3049307f, 0.8066275f),
		new Vector2(0.09958661f, 0.4840356f),
		new Vector2(0.150175f, -0.04598744f),
		new Vector2(0.3423871f, 0.3214795f),
		new Vector2(0.6655422f, -0.2328113f),
		new Vector2(0.4823369f, -0.32793f),
		new Vector2(-0.8112878f, -0.3319037f),
		new Vector2(-0.1866378f, -0.2180844f),
		new Vector2(0.253612f, -0.1839722f),
		new Vector2(-0.8187047f, -0.1960776f),
		new Vector2(0.01073296f, -0.6189364f),
		new Vector2(0.2253672f, 0.8513179f),
		new Vector2(0.5340158f, -0.6310921f),
		new Vector2(0.2562835f, 0.2177237f),
		new Vector2(0.6707539f, 0.132519f),
		new Vector2(0.1067485f, 0.9525791f),
		new Vector2(0.3476667f, -0.6349633f),
		new Vector2(0.5992626f, -0.4731912f),
		new Vector2(-0.2887911f, 0.1155632f),
		new Vector2(-0.7777544f, -0.5463549f),
		new Vector2(0.1611587f, -0.809207f),
		new Vector2(-0.186726f, -0.6633827f),
		new Vector2(-0.3539035f, 0.169837f),
		new Vector2(-0.7356462f, 0.4256332f),
		new Vector2(-0.7520303f, -0.424752f),
		new Vector2(-0.06047862f, 0.8165238f),
		new Vector2(-0.3239925f, -0.5418949f),
		new Vector2(-0.4328813f, -0.05714953f),
		new Vector2(0.1239775f, 0.6442645f),
		new Vector2(0.2108501f, -0.4523228f),
		new Vector2(0.1879562f, 0.1348673f),
		new Vector2(0.2990914f, 0.07977276f),
		new Vector2(0.222966f, -0.2303208f),
		new Vector2(-0.6494362f, 0.1557336f),
		new Vector2(0.2366887f, 0.02996339f),
		new Vector2(-0.2502881f, -0.3681093f),
		new Vector2(0.7183835f, 0.2804115f),
		new Vector2(-0.1820996f, -0.7746923f),
		new Vector2(0.2700185f, -0.4202308f),
		new Vector2(0.8284113f, 0.4146816f),
		new Vector2(0.3020513f, -0.3048345f),
		new Vector2(0.3862495f, 0.02021868f),
		new Vector2(0.8679589f, -0.2260808f),
		new Vector2(0.849471f, -0.5052767f),
		new Vector2(-0.07456715f, -0.5169069f),
		new Vector2(0.2918289f, 0.2740719f),
		new Vector2(0.4110489f, -0.322429f),
		new Vector2(-0.4213311f, -0.2011903f),
		new Vector2(-0.3678172f, 0.04718375f),
		new Vector2(0.2193468f, -0.311746f),
		new Vector2(-0.7583115f, 0.0760784f),
		new Vector2(-0.6576787f, 0.1120576f),
		new Vector2(-0.5048372f, 0.388713f),
		new Vector2(-0.8801907f, 0.3463418f),
		new Vector2(-0.4107973f, 0.6618397f),
		new Vector2(0.02040408f, -0.5752473f),
		new Vector2(-0.610888f, 0.4725673f),
		new Vector2(0.1437651f, 0.7029845f),
		new Vector2(0.6513476f, 0.3163535f),
		new Vector2(-0.1073342f, -0.8175783f),
		new Vector2(0.856895f, 0.07427894f),
		new Vector2(-0.04060197f, 0.1883817f),
		new Vector2(0.188414f, -0.7471575f),
		new Vector2(-0.6934884f, -0.1666583f),
		new Vector2(0.2675523f, 0.814865f),
		new Vector2(-0.7667408f, -0.5694765f),
		new Vector2(0.2194138f, 0.1705861f),
		new Vector2(0.08955999f, 0.8800545f),
		new Vector2(-0.1045938f, 0.2754561f),
		new Vector2(-0.3462243f, 0.1693828f),
		new Vector2(0.5123719f, -0.1304053f),
		new Vector2(-0.7039207f, -0.49536f),
		new Vector2(-0.5383397f, -0.07155029f),
		new Vector2(0.1283657f, 0.8273883f),
		new Vector2(-0.922888f, -0.2587045f),
		new Vector2(-0.1324336f, -0.4743302f),
		new Vector2(-0.03312912f, 0.1497381f),
		new Vector2(-0.8265015f, 0.01711409f),
		new Vector2(0.5512118f, 0.4629506f),
		new Vector2(-0.1624323f, 0.5872278f),
		new Vector2(-0.03847997f, 0.4235697f),
		new Vector2(0.06227373f, -0.1027207f),
		new Vector2(-0.3131243f, 0.3446279f),
		new Vector2(0.79188f, -0.2432595f),
		new Vector2(0.3101794f, 0.8572418f),
		new Vector2(-0.0806587f, -0.2529583f),
		new Vector2(0.6410898f, -0.6631638f),
		new Vector2(-0.2852911f, -0.3291907f),
		new Vector2(0.8461984f, 0.2483173f),
		new Vector2(-0.5120467f, 0.6785628f),
		new Vector2(-0.3012265f, -0.7601035f),
		new Vector2(-0.1851587f, -0.5389704f),
		new Vector2(0.7883444f, 0.02091975f),
		new Vector2(0.8209827f, 0.4387864f),
		new Vector2(-0.2608053f, -0.6634251f),
		new Vector2(0.8798827f, 0.1980502f),
		new Vector2(-0.4492102f, -0.01876298f),
		new Vector2(-0.2135147f, -0.4665161f),
		new Vector2(0.7211974f, 0.02973793f),
		new Vector2(0.3683178f, 0.8347923f),
		new Vector2(-0.3564104f, 0.4551469f),
		new Vector2(0.1298598f, 0.5669253f),
		new Vector2(-0.263394f, -0.4390568f),
		new Vector2(0.6591117f, -0.5724326f),
		new Vector2(0.3257563f, -0.6894131f),
		new Vector2(-0.3622783f, 0.1078354f),
		new Vector2(-0.01651493f, -0.9807722f),
		new Vector2(-0.1285588f, -0.558165f),
		new Vector2(-0.007965654f, 0.4789495f),
		new Vector2(0.09269772f, 0.6924497f),
		new Vector2(0.5143246f, 0.2415018f),
		new Vector2(0.7062011f, -0.6039186f),
		new Vector2(0.3208524f, 0.3235568f),
		new Vector2(-0.22608f, 0.2385225f),
		new Vector2(-0.002344047f, -0.8300962f),
		new Vector2(0.4534542f, 0.4212298f),
		new Vector2(-0.3716744f, 0.2729925f),
		new Vector2(0.1148906f, -0.6977009f),
		new Vector2(-0.2311537f, -0.6227176f),
		new Vector2(-0.5941743f, 0.2652791f),
		new Vector2(0.3872949f, 0.5007054f),
		new Vector2(-0.79018f, -0.5685303f),
		new Vector2(0.8604763f, 0.1571719f),
		new Vector2(-0.2781825f, 0.2811884f),
		new Vector2(0.7269484f, -0.5416591f),
		new Vector2(0.6422624f, 0.2901808f),
		new Vector2(-0.1926293f, -0.7472127f),
		new Vector2(-0.1533057f, -0.8349765f),
		new Vector2(0.2589435f, -0.7054861f),
		new Vector2(0.3596774f, 0.6092417f),
		new Vector2(-0.05385771f, -0.7675181f),
		new Vector2(-0.8074552f, -0.2224952f),
		new Vector2(0.2608257f, 0.739048f),
		new Vector2(0.6472324f, 0.6112146f),
		new Vector2(-0.1967537f, 0.8481473f),
		new Vector2(0.1765839f, 0.9154196f),
		new Vector2(0.136836f, 0.4681451f),
		new Vector2(0.1656491f, 0.4355212f),
		new Vector2(0.03508705f, 0.5783172f),
		new Vector2(0.287016f, 0.6881996f),
		new Vector2(-0.6854144f, -0.3541684f),
		new Vector2(-0.1766913f, -0.7308956f),
		new Vector2(-0.757535f, -0.1297268f),
		new Vector2(-0.1089054f, 0.8976627f),
		new Vector2(-0.2606815f, -0.3597739f),
		new Vector2(0.01673449f, -0.4795828f),
		new Vector2(-0.1200767f, -0.6408895f),
		new Vector2(0.2728124f, 0.3583335f),
		new Vector2(0.2471238f, 0.9686117f),
		new Vector2(-0.1221448f, -0.6493783f),
		new Vector2(-0.8002294f, 0.5912706f),
		new Vector2(-0.6024586f, 0.07578965f),
		new Vector2(0.186485f, 0.2984653f),
		new Vector2(-0.5617874f, -0.1502944f),
		new Vector2(0.3201214f, 0.3586649f),
		new Vector2(-0.2724754f, 0.375663f),
		new Vector2(0.1822144f, -0.5944112f),
		new Vector2(-0.07433558f, -0.5242764f),
		new Vector2(0.2621156f, -0.3537353f),
		new Vector2(-0.009411166f, 0.9015301f),
		new Vector2(-0.4617579f, 0.3042763f),
		new Vector2(0.3098396f, -0.5446912f),
		new Vector2(-0.3316887f, -0.8092509f),
		new Vector2(-0.600909f, -0.3858983f),
		new Vector2(-0.05354552f, 0.808335f),
		new Vector2(0.11999f, 0.4426292f),
		new Vector2(-0.01637966f, 0.7920868f),
		new Vector2(-0.5302508f, -0.2493226f),
		new Vector2(0.4636287f, -0.1451893f),
		new Vector2(0.346733f, -0.8741296f),
		new Vector2(0.2719175f, 0.3948368f),
		new Vector2(0.2916211f, 0.2689691f),
		new Vector2(-0.3236818f, -0.3991515f),
		new Vector2(0.05204542f, 0.7334583f),
		new Vector2(-0.1108919f, -0.1813408f),
		new Vector2(-0.4574054f, -0.4723869f),
		new Vector2(-0.4323944f, -0.5140581f),
		new Vector2(0.8244372f, -0.5654368f),
		new Vector2(-0.2816502f, 0.9121416f),
		new Vector2(0.8176693f, 0.552748f),
		new Vector2(-0.501299f, 0.04976126f),
		new Vector2(-0.02750311f, -0.5081977f),
		new Vector2(-0.0008421738f, 0.6408225f),
		new Vector2(-0.3271355f, -0.6724291f),
		new Vector2(0.5254803f, 0.2268418f),
		new Vector2(-0.3891181f, -0.05082186f),
		new Vector2(-0.1969595f, -0.5834324f),
		new Vector2(-0.9442863f, 0.3006329f),
		new Vector2(-0.6217294f, -0.5021245f),
		new Vector2(0.06346669f, -0.6570222f),
		new Vector2(-0.2546797f, -0.3472827f),
		new Vector2(0.01211904f, 0.8803958f),
		new Vector2(0.4602985f, 0.1220791f),
		new Vector2(-0.9988728f, 0.007000204f),
		new Vector2(-0.2071004f, 0.6365714f),
		new Vector2(0.7046911f, 0.5453223f),
		new Vector2(0.5182525f, -0.1497447f),
		new Vector2(-0.7953118f, 0.5112747f),
		new Vector2(0.6612972f, 0.07837194f),
		new Vector2(-0.733006f, -0.5465791f),
		new Vector2(-0.6951305f, -0.24291f),
		new Vector2(-0.299214f, -0.2042961f),
		new Vector2(-0.6844003f, -0.5033525f),
		new Vector2(-0.1880909f, 0.0410551f),
		new Vector2(0.205771f, -0.5928177f),
		new Vector2(-0.2205501f, -0.651386f),
		new Vector2(-0.1346016f, -0.4247628f),
		new Vector2(-0.5397924f, 0.5137258f),
		new Vector2(-0.937448f, 0.2475442f),
		new Vector2(-0.03857157f, 0.545296f),
		new Vector2(0.2004336f, 0.5579207f),
		new Vector2(-0.5981974f, -0.4889902f),
		new Vector2(-0.1169676f, 0.441873f)
	};

	public static Vector2 insideUnitCircle
    {
        get
        {
			return unitCircles[Random.Range(0, unitCircles.Length)];
        }
    }

	public static Vector3 vector01
    {
        get
        {
			return new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}