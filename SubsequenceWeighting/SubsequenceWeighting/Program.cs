using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsequenceWeighting
{
    public class ATree
    {
        private List<int> tree;
        ATree(int root)
        {
            tree = new List<int>();
            tree.Add(root);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //string T = Console.In.ReadLine();
            
            //int t = int.Parse(T);
            //int loop = 0;
            //while( (t > 0 && t < 6) && loop < t) 
            //{
            //    //string raw_ai = "189537003	646195294	188957657	211894129	567127252	704895562	621613703	642503614	320028885	734851752	388209824	271833712	293050515	165068638	645249482	791980356	637203908	858008349	69195290	949943855	73983085	941848874	837543675	373842956	398129867	58932598	354822560	478293596	412844256	332096104	534104498	969146446	548901050	24174251	585285224	667740313	38197850	357664590	367861605	592786743	482089153	986552366	36435029	328799106	726804371	19097314	536030251	445180795	238890275	43668658	130862356	751984010	493252318	816171467	531952137	542305565	553840576	678358165	815114644	356777579	150099690	440292945	950431420	436818687	661930774	436943667	852372409	948643888	579802055	925052019	396877655	125663542	136361995	371136066	210551651	354231556	679378812	23399405	544418151	355143546	967467819	120606991	66450418	15560076	167233330	713410161	702066516	19154654	65478015	110109780	897992354	612950710	954968161	44566675	288353997	89524267	15840502	691031622	753389679	619790531	174674394	283499253	628150910	603099511	332822951	452242308	630634172	216313812	938997935	395612218	324215080	232932086	538851900	690587593	999496269	137387133	260688359	288305336	772400072	110986522	31379527	259904961	931440941	784380764	799314738	341836996	603802679	191707784	688828012	708541259	39581513	384211136	613719057	352447390	138318637	760094485	365677520	593400462	581903254	340095293	605732309	627685084	107903394	901735952	633638588	338793368	844074330	760589731	95821604	183419548	657543146	58198110	621117266	192681673	292593633	977187922	302721345	772737134	182415005	557951365	532835764	374752044	582811352	460284173	688351135	148278433	355006659	791879450	619745451	741985774	802315102	351548419	325985383	312770346	878519160	965118530	33183002	555866802	270497635	237764263	505934175	393783216	259811109	957201671	794549465	43003727	720073431	912753403	451301501	802952379	334485345	248823051	60867153	989606702	576118919	900507647	675446784	397577732	455930471	427937688	998599489	849845451	820499067	180172315	696807868	188364775	80240892	326486463	609200866	513975110	124927638	409561951	769035685	93065381	507317889	898568453	677715059	617147807	871236874	535827042	509641191	362920479	486925014	516750778	920351049	663958820	726706475	775931555	477573000	91659368	633881886	498311560	151861871	474492469	891607599	30542191	381456872	47895564	995893448	500872489	931166720	485395755	264147833	833703913	792814811	997916838	764285405	227640364	457588502	353851392	496404342	682427140	882497538	367560339	879131606	873277826	320254305	709491220	969779221	879093514	424153157	551000741	543194404	744630072	69153195	12611407	220242933	69454155	932192986	135124337	168110610	736960318	520228942	806866509	472505809	789902766	286750427	976051726	531646967	767626886	465885224	732795004	8577163	356519170	40612898	177240027	568130891	823173262	686977835	937832130	580753197	691500827	341959886	887812206	824888892	41590003	844784212	33400964	838249495	535039228	806905837	218964008	710876366	704062730	694392266	139409974	63845493	566893404	740947577	500270573	624275899	752124745	148492512	865186952	260132269	611046369	216709318	24438479	135817621	758743328	701838713	848691815	211330433	122797790	946095727	980975390	961458097	844684360	893662666	452563082	599574582	998361433	951960309	180320298	410008472	588791404	404710850	729728471	51998991	479967629	58581531	419577959	442063937	458804262	222928085	814132115	166112725	251076809	290284248	229708611	247206115	305409004	192160984	461467081	318915448	916723305	362206289	493145502	366055796	320782134	713590379	872025731	609888405	482842447	680843290	35660261	927793156	519190773	166232932	460982320	70112609	216748032	945823572	442883434	667556969	40743475	131942635	695601452	11333600	524964734	922370406	705341534	938338670	941020021	385364619	444790558	17364327	558910129	354308840	37245251	381958942	118302815	722505765	354175248	103348579	599260299	496008296	899097980	415218523	47995645	859894965	216466408	180917220	92704973	176561439	146411377	513317267	596334782	447192572	823851284	900209308	949834401	981032134	453733261	212625282	447613161	798881152	296107013	836178791	739802284	682586523	515141672	975914580	554167967	85532389	201292418	962949792	453863502	521731773	382213153	382602738	735364675	525818461	941289636	77913391	966258156	572905095	184589466	516165627	547866004	489357411	974940178	31429756	404027460	212132385	727635364	979001055	953738716	331773180	207860942	924465137	419168875	203202583	961405453	957472941	217583766	324566048	35289546	499171720	783526555	243526695	406814774	916157344	795987574	391107324	747739065	430245083	725446107	315274507	767082161	834208857	920021330	610240683	511813415	129821841	977609993	681094338	952856293	282581297	428904644	197764350	84245539	971216779	169688195	807743076	127055097	806959745	745789763	75090575	505847528	369163691	944313089	954196775	696307353	293960930	672103564	661360889	700240795	87705752	801724529	799181460	557216919	571874071	991361124	565920080	954376786	84738918	48778362	127615242	369063750	185267816	922571958	933173507	218726528	869745175	750425792	8526759	814814461	676186083	719580212	591127238	483512536	114655911	768763606	653200689	251644406	374408438	395887052	824817429	688247947	215624807	804238305	297685468	350752964	593117245	989633184	161737419	408869147	282589469	44525084	875091019	773737314	446701429	663397645	995567444	526494291	388684444	732985850	734709324	216310008	478890992	493521170	666402769	683191661	669069224	277522545	266138507	959209593	385908150	6663712	975371604	39878282	544908503	913562245	889033978	145197499	552331513	501957072	663640452	338043573	482064477	376712823	997505205	102540809	831616516	717552413	182172110	143472370	290310916	946759392	469450810	222430314	451722139	381596888	756334801	690004884	89357503	627547997	309407188	647825885	693213040	853055233	325210294	344114483	685591078	328790981	854394328	875214592	38043088	261149570	110845612	862652498	536646110	644780637	165902844	418071986	291984431	431997802	892104061	447266280	974973451	946824727	808652311	280949876	784139326	482262046	248435511	764117228	962192130	735430672	271728362	711022467	678061864	572023002	450994834	575367457	197446094	721711322	430379666	603511442	735723829	759026750	569963286	203572277	327429959	808248479	425348473	725846380	431121727	686425551	957166931	710683183	28426628	56754315	571817209	596451309	619708831	984520779	586019259	622809511	137521229	572564237	336020633	443927938	304161305	114263953	450469346	149357829	442113933	867711990	416671973	628833931	952896859	560668188	356345221	845673796	953523659	923771972	844690253	225311769	870123370	754292689	463264834	489306568	203216907	765066070	866760451	922910837";
            //    //string raw_wi = "213349906\t574839758\t237154603\t430343669	715795280	821874267	127944826	283530966	893538662	633280121	107806178	337778927	686782896	217074328	531654947	868549765	167612098	795230881	598637197	908844853	644789236	995277542	942101168	115335492	181372749	138984274	47722012	211510476	366283841	122613562	415005191	280452720	983311442	767402990	35578589	840153566	330913013	794390137	244234297	426912337	797451816	263748316	439450761	959467758	316698401	382535704	924758721	825326750	822783679	534631579	290496542	423880866	366598737	79544470	902964812	777594320	366893693	920797173	877346544	543940358	245082697	574985485	921402933	891466132	137191389	681952406	867816300	674391442	364118197	57618034	761984036	342303495	339210504	117520819	221400244	925080148	768288680	238650185	21298458	52104968	847342218	558424244	256194807	979678199	967430320	739474929	874857573	411994470	595499029	86220784	367691142	913661237	735716576	629568556	929789668	24336684	31136141	606400696	871118510	818026943	309270209	463453742	297509099	904855897	368129824	537570032	305981066	324897285	130102460	126326924	423451159	89847893	471159110	425945027	450932135	39716092	63089754	119313529	697429868	195319892	366959416	596584090	225232079	510369224	963513850	8276908	479858044	344085445	170740142	843835846	753159634	297007689	236309956	913947811	717837783	982420455	581449498	863586625	256510774	523365062	204610567	274675702	452007953	721615322	623205029	636505398	806580653	580693656	750983127	170794515	46002663	932664707	96870624	634850851	193247584	538369190	17492438	838501699	926647061	458725403	172501631	410530896	443510001	837055858	320880315	572978628	801318908	677115827	507630882	604675445	689903434	871234580	351774287	390364048	19188531	40055006	897515116	286110012	509680621	961779569	884772501	155256807	380632964	41929023	458788984	486084215	993856835	308485653	627013281	685051477	112971276	377170166	39014516	868289125	855752159	163994611	305948167	195144527	655782916	985302567	751899850	770677253	88135721	43140419	275966273	102361889	670138775	547141197	890752660	277263978	118801899	42462109	89686982	489942658	382123034	571795070	637005951	656542242	943783928	626703489	218725899	314313196	6504168	554168289	426393659	623837681	154297455	565898348	354940807	983268583	315143361	415541439	880886498	145337959	95428676	609556690	273595569	619443966	419773469	218350041	722887326	535922670	714537439	947581128	101859288	509763197	985131990	38869905	120364809	505629081	622994234	863457047	120278499	941689372	795243974	62921921	706468847	152662809	743102331	748467698	734934226	26683679	316209700	17818785	612132854	511450439	378361615	695018114	562679636	507862087	487176562	665863409	539026580	230080937	80886553	806103873	401261877	732962899	927688952	431345168	675490717	889143	349870251	537817879	689240456	463824675	562976004	509590814	960074358	622436263	197129170	984216212	879406573	533934437	519706664	36658794	803435378	450691481	164583176	898869249	317243478	36823537	957824638	959647049	973944003	696011890	129913367	694694874	494762100	148678248	358075512	261097088	112860302	805201865	129876477	913355607	580616147	918369261	30848564	326553440	597491594	457151827	699760365	133745895	134489542	48769077	692217073	586433871	889632822	27065709	390998175	619979871	226461433	195025726	161931049	277785137	710389784	827158556	709219344	592938208	906762793	566989440	522161902	175381827	429853134	961048899	836312331	752926598	784428074	189686291	148356508	315659183	731508166	755838186	751559413	653173238	873499807	722666585	595476905	456447332	38293852	30910889	489150953	463223057	338253072	586782727	769948341	387004246	399200585	81987098	738822089	587427637	800645480	144251686	317153999	460876585	931880091	988380007	811883618	673411049	122273324	455857053	28766320	810765264	760958387	712231481	184245642	280649208	152191189	127311909	106623607	222309294	879656495	544466173	828501282	381352882	8947373	46289893	354262015	766753112	547903114	26878246	530178617	678953393	549112963	179488699	365632640	528750981	479046364	131032382	346213166	312381803	45622739	842122206	44878248	981797354	814399480	771449996	570816838	836394100	43814657	438437171	862399861	530451082	42907739	93147258	111288167	99397549	715682631	251290998	207560676	586993777	162237191	215123522	93445328	278927884	430075998	190968620	559749056	478579681	370584279	92481790	879677701	743373490	627500491	15390684	197425642	255247544	835003120	92480775	232534151	875279531	651969097	998588403	117288744	206856288	48831742	604532135	104806967	638416281	854241814	578519021	809200381	911221461	105649163	298728997	901850351	218090863	315474802	789085167	423517856	248427466	221600760	141642157	780519091	192670869	717201824	792965104	975263603	172011834	364140780	728549273	908530949	706929381	777289979	485929698	500995523	764697871	167900026	9894543	602408462	608009393	226256446	634469569	313381903	515071314	6833392	98710791	66315654	916514619	754643405	438889296	834247506	140787352	680721964	437809697	170493405	445145530	582595381	745256129	137285384	939910529	986053491	428655427	998077952	987442098	797061638	81108005	51058564	559040006	401606064	381204990	501475019	523411435	210372429	865574158	651470940	590373894	161049562	213715257	520433918	388298340	145760736	528491059	943022876	61820536	238567099	442710103	710247844	822455085	792511132	616413794	683759842	329124425	210645703	79722693	433131326	879974668	318254824	550462416	869033232	788834010	258577377	323793582	369791619	726929554	548540213	622606969	363562354	959499912	316822684	948645552	931199538	416498970	810214633	626586949	706901481	393226009	742087464	388065393	532381112	716854129	948536771	486658692	340255830	20305519	464438567	922913437	833262027	422598398	115904217	923366657	344997063	760251157	957672776	368780422	506527064	79052165	491540846	269573687	633570615	440411367	194528633	391025223	580880170	993954600	94413329	471318810	50484422	635295144	565579836	139170160	943870304	15789113	901328914	582920674	587927248	819082960	497106737	701062165	65245095	75665753	186358051	596708800	513313623	406292320	384224540	28428668	740133608	106315729	993288684	499097343	301759979	85863061	609260486	934571245	203605839	497491401	428473008	2548446	323051947	408327997	834406886	654853373	957318425	754480476	335427524	562167981	872531592	145032312	251352598	240065314	295108790	246405626	705085846	426891886	641084385	672027613	447310339	883733622	705381251	812591670	432171479	245754844	734242164	161034436	511579282	759928392	641120030	998680084	708415804	531783902	493206358	657985322	509948092	326354762	341500361	134954179	528609224	626696544	414153935	408552022	243217909	864535049	538345365	37566195\t2652455\t590401292" ;

            //    string raw_ai = "1\t2\t3\t4\t1\t2\t3\t4";
            //    string raw_wi = "10\t20\t30\t40\t15\t15\t15\t50";


            //    //string N = Console.In.ReadLine();
            //    //string raw_ai = Console.In.ReadLine();
            //    //string raw_wi = Console.In.ReadLine();

            //    int size = 8;//678;//int.Parse(N);

            //    var Ai = raw_ai.Split('\t');
            //    var Wi = raw_wi.Split('\t');

            //    Dictionary<int, int> Bu;
            //    if ( (Wi.Count() == size) && (Ai.Count() == size) )
            //    {
            //        //Solution1(Ai, Wi);
            //    }
            //    loop++;
            //}
        }

        private static void Solution1(string[] Ai, string[] Wi)
        {
            long maxweight = 0;

            List<int> Bi;
            int startingIndex = 0;

            while (startingIndex < Ai.Count() && startingIndex >= 0)
            {
                long weight = 0;

                int nextIndex = CalculatedSubSeqWeight(startingIndex, Array.ConvertAll(Ai, Int32.Parse), Array.ConvertAll(Wi, Int32.Parse), out Bi);

                foreach (var key in Bi)
                {
                    weight += int.Parse(Wi[key]);
                }

                if (maxweight < weight)
                {
                    maxweight = weight;
                }

                startingIndex = nextIndex;
            }

            Console.Out.WriteLineAsync(maxweight.ToString());
        }



        public static Dictionary<int, List<int>> GenerateWeightTableUnsorted(int[] Ai, int[] Wi)
        {
            Dictionary<int, List<int>> weightTable = new Dictionary<int, List<int>>();

            // make i (1 <= i < M), bi < bi + 1
            for (int i = 0; i < Ai.Length && i < Wi.Length; i++)
            {
                if (!weightTable.ContainsKey(Ai[i]))
                {
                    weightTable.Add(Ai[i], new List<int>() { i });
                }
                else
                {
                    weightTable[Ai[i]].Add(i);
                }
            }
            return weightTable;
        }


        public static SortedDictionary<int, List<int>> GenerateWeightTableOptimize(int[] Ai, int[] Wi)
        {
            SortedDictionary<int, List<int>> weightTable = new SortedDictionary<int, List<int>>();
  
            // make i (1 <= i < M), bi < bi + 1
            for (int i = 0; i < Ai.Length && i < Wi.Length; i++)
            {
                if (!weightTable.ContainsKey(Ai[i]))
                {
                    weightTable.Add(Ai[i], new List<int>() { i });
                }
                else
                {
                    weightTable[Ai[i]].Add(i);
                }
            }
            return weightTable;
        }


        public static Dictionary<int, int[]> GenerateWeightTable(int[] Ai, int[] Wi)
        {
            List<int> Bi;
            Dictionary<int, int[]> weightTable = new Dictionary<int, int[]>();
            int startingIndex = 0;
            int nextIndex = 0;
            while (startingIndex < Ai.Length && startingIndex >= 0)
            {
                if (!weightTable.ContainsKey(Ai[startingIndex]))
                {
                    nextIndex = Program.FindAllMatchingPairs(startingIndex, Ai, Wi, out Bi);
                    weightTable.Add(Ai[startingIndex], Bi.ToArray());
                }
                else
                {
                    nextIndex++;
                }
                startingIndex = nextIndex;
            }
            return weightTable;
        }

        /// <summary>
        /// Return the subsequence (b,w) given a start index 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="Ai"></param>
        /// <param name="Wi"></param>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static int FindAllMatchingPairs(int startIndex, int[] Ai, int[] Wi, out List<int> bu)
        {
            
            bu = new List<int>();
            //start at index 0
            bu.Add(startIndex);
            int nextIndex = -1;
           
            // make i (1 <= i < M), bi < bi + 1
            for (int i = startIndex + 1; i < Ai.Length && i < Wi.Length; i++)
            {
                // Find all pair with the same A[i]
                if (Ai[startIndex] == Ai[i])
                {
                    bu.Add(i);
                }
                else
                {
                    if (nextIndex == -1)
                    {
                        nextIndex = i;
                    }
                }
            }

            return nextIndex;
        }

        /// <summary>
        /// Return the subsequence (b,w) given a start index 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="Ai"></param>
        /// <param name="Wi"></param>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static int CalculatedSubSeqWeight(int startIndex, int[] Ai, int[] Wi, out List<int> bi)
        {
            bi = new List<int>();
            //start at index 0
            bi.Add(startIndex);
            int nextIndex = -1;
            // make i (1 <= i < M), bi < bi + 1
            for (int i = startIndex + 1; i < Ai.Length && i < Wi.Length; i++)
            {
                // check the first item on the pair
                int last = bi.Last();
                if (Ai[last] < Ai[i])
                {
                    bi.Add(i);
                }
                else if (Ai[last] == Ai[i])
                {
                    // opportunity to swap for a higher weight with the same "a"
                    // check the second item on the pair
                    if (Wi[last] < Wi[i])
                    {
                        bi[bi.Count-1] = i;
                    }
                }
                else
                {
                    if (nextIndex < 0)
                    {
                        // revisit this for the next subsequence
                        nextIndex = i;
                    }

                }
            }
            return nextIndex;
        }


        /// <summary>
        /// Return the subsequence (b,w) given a start index 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="Ai"></param>
        /// <param name="Wi"></param>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static long CalculateSubSeqweight(List<int> subsequence, int[] Ai, int[] Wi,out List<int> wu)
        {
            long weight = 0;
            wu = new List<int>();
            for (int i = 0, j = 0; i < Ai.Length && i < Wi.Length; i++)
            {
                if (subsequence[j] == Ai[i])
                {
                    weight += Wi[i];
                    wu.Add(Wi[i]);
                    j++;
                }
            }
            return weight;
        }

        public static IEnumerable<int> GetNode(List<int> nodelist)
        {
            for (int i = 0; i < nodelist.Count; i++)
            {
                yield return nodelist[i];
            }
        }

        public static int[] TraverseNodes(IDictionary<int, List<int>> rows, IList<int> visitedNodes)
        {
            int levelElementIndexer = 0;
            IList<int> subsequence = new List<int>();
            var nodes = rows.Keys.GetEnumerator();
            
            while (nodes.MoveNext())
            {
                var level = rows[nodes.Current];

                if(visitedNodes.Contains(level[levelElementIndexer]))
                {
                    levelElementIndexer++;
                }

                // for each level count the number of element for this level
                while (level.Count > 0)
                {    
                    if (subsequence.Count == 0 || subsequence.Last() < level[levelElementIndexer])
                    {
                        subsequence.Add(level[levelElementIndexer]);
                        levelElementIndexer = 0;
                        break;
                    }
                    else
                    {
                        levelElementIndexer++;
                    }
                }
            }
            return subsequence.ToArray();
        }


        public static long TraverseMaxNodes(IDictionary<int, List<int>> weightTable, int[] Wi, int rootnodeoffset = 0)
        {
            long maxWeight = 0;
            int indexMaxWeight = 0;
            int levelElementIndexer = 0;
            List<int> subsequence = new List<int>();

            var nodes = weightTable.Keys.GetEnumerator();
            nodes.MoveNext();
            subsequence.Add(weightTable[nodes.Current].ElementAt(rootnodeoffset));
            maxWeight += Wi[subsequence.Last()];

            while (nodes.MoveNext())
            {
                int localmax = 0;
                var level = weightTable[nodes.Current];
                
                // for each level count the number of element for this level
                while (level.Count > 0 && levelElementIndexer < level.Count)
                {
                    //Find the maximum
                    if (Wi[level[levelElementIndexer]] > localmax)
                    {
                        localmax = Wi[level[levelElementIndexer]];
                        indexMaxWeight = levelElementIndexer;
                    }
                    else if ((indexMaxWeight < levelElementIndexer) && (subsequence.Count != 0))
                    {
                        // choice the index greater
                        // the index of the max weight is smaller than the last element of subsequence of index
                        if (subsequence[subsequence.Count - 1] < level[levelElementIndexer]
                            && subsequence[subsequence.Count - 1] > level[indexMaxWeight] )
                        {
                            indexMaxWeight = levelElementIndexer;
                        }
                    }
                    levelElementIndexer++;
                }

                if (subsequence.Count == 0 || subsequence[subsequence.Count - 1] < level[indexMaxWeight])
                {
                    // add to the list of subsequence of index
                    subsequence.Add(level[indexMaxWeight]);
                    maxWeight += Wi[level[indexMaxWeight]];
                    levelElementIndexer = 0;
                    indexMaxWeight = 0;
                    localmax = 0;
                }
            }

            var ans = subsequence.ToArray();

            return maxWeight;
        }

        public static List<int> GetLongestSubSequence(int[] Ai)
        {
            List<List<int>> seqLists = new List<List<int>>();

            for (int i = 0; i < Ai.Length; i++)
            {
                List<int> seq = Program.CalculateSubsequence(Ai, i);
                seqLists.Add(seq);
                i = seq[seq.Count - 1];
            }

            int seqlength = 0;
            int seqlindex = 0;
            // Look for the largest sequence length
            for (int i = 0; i < seqLists.Count; i++)
            {
                if (seqLists[i].Count > seqlength)
                {
                    seqlength = seqLists[i].Count;
                    seqlindex = i;
                }
            }


            int dropped = 0;


            var seqListsCopy = seqLists.ToList();

            var longSeq = seqLists[seqlindex].ToList();

            var firstseq = longSeq.ToList();



            while (dropped < longSeq.Count)
            {
                for (int i = 1; i < seqListsCopy.Count; i++)
                {
                    if (Ai[firstseq.Last()] > Ai[seqListsCopy[i].First()])
                    {
                        continue;
                    }
                    else
                    {
                        if (dropped < firstseq.Count)
                        {
                            longSeq = firstseq.ToList();
                            longSeq.AddRange(seqListsCopy[i]);
                            firstseq = longSeq.ToList();
                        }

                        seqListsCopy.RemoveAt(i);
                        dropped = 0;
                        // extend the "first seq"
                        break;
                    }

                }



                firstseq.RemoveAt(firstseq.Count - 1);
                dropped++;
            }


            //for ( int i = 1; i < seqLists.Count ; i++)
            //{
            //    var firstseq = longSeq.ToList();
            //    while (dropped < seqLists[i].Count)
            //    {
            //        // 
            //        if (Ai[firstseq.Last()] > Ai[seqLists[i].First()])
            //        {
            //            dropped++;
            //            firstseq.RemoveAt(firstseq.Count - 1);
            //        }
            //        else
            //        {
            //            longSeq = firstseq.ToList();
            //            longSeq.AddRange(seqLists[i]);
            //            // extend the "first seq"
            //            break;
            //        }
            //    }
            //    dropped = 0;
            //}

            return longSeq;
        }
        
        public static List<int> CalculateSubsequence(int[] Ai, int offset = 0)
        {
            List<int> sequence = new List<int>();

            for (int i = offset; i < Ai.Length; i++)
            {
                if (sequence.Count == 0)
                {
                    sequence.Add(i);
                }
                else
                {
                    if (Ai[sequence.Last()] < Ai[i])
                    {
                        sequence.Add(i);
                    }
                }
            }

            return sequence;
        }
        
        public static List<int> CalculateSubsequenceWithRemainder(int[] Ai, out IList<int> remainder)
        {
            List<int> sequence = new List<int>();
            remainder = new List<int>();
            for (int i = 0; i < Ai.Length; i++)
            {
                if (sequence.Count == 0)
                {
                    sequence.Add(Ai[i]);
                }
                else
                {
                    if (sequence.Last() < Ai[i])
                    {
                        sequence.Add(Ai[i]);
                    }
                    else
                    {
                        remainder.Add(Ai[i]);
                    }
                }
            }

            return sequence;
        }

        public static List<int> FindLongestSubsequence(int [] Ai, int [] Wi)
        {
            List<int> seq = new List<int>();
            List<List<int>> seqs = new List<List<int>>();
            IList<int> remainder;

            seqs.Add(Program.CalculateSubsequenceWithRemainder(Ai, out remainder));

            while (true)
            {
                if (remainder.Count < seqs.Last().Count)
                {
                    break;
                }
                else
                {
                    seqs.Add(Program.CalculateSubsequenceWithRemainder(remainder.ToArray(), out remainder));
                }
            }

            // find the largest subseqence weight
            
            int len = 0;
            int itemIndex = 0;
            int indexer = 0;
            int maxWeight = 0;
            foreach (var subseq in seqs)
            {
                //Premise 2:
                //int weightedsum = 0;
                //foreach (var element in subseq)
                //{
                //    weightedsum += Wi[Array.IndexOf(Ai, element)];
                //}

                //if (weightedsum > maxWeight)
                //{
                //    maxWeight = weightedsum;
                //    itemIndex = indexer;
                //}


                // Premise 1: not true
                // this does not work
                if (subseq.Count > len)
                {
                    seq = subseq.ToList();
                    len = subseq.Count;
                    itemIndex = indexer;
                }
                indexer++;
            }


            seqs.RemoveAt(itemIndex);

            seq = AppendToSubSequence(Ai, seq, seqs);
                        
            return seq;
        }

        private static List<int> AppendToSubSequence(int[] Ai, List<int> seq, List<List<int>> seqs)
        {
            List<int> beginPart = new List<int>();
            List<int> endPart = new List<int>();
            List<int> middlePart = new List<int>();

            foreach (var subseq in seqs)
            {
                beginPart.AddRange(subseq.FindAll(a => a < seq.First()));
                middlePart.AddRange(subseq.FindAll(a => a > seq.First() && a < seq.Last())); 
                endPart.AddRange(subseq.FindAll(a => a > seq.Last()));
            }

            //sort
            int lowerindexbound = Array.IndexOf(Ai, seq.First());
            int upperindexbound = Array.IndexOf(Ai, seq.Last());

            // append
            List<int> lowerindexmap = new List<int>();
            List<int> middleindexmap = new List<int>();
            List<int> upperindexmap = new List<int>();

            foreach (var a in beginPart)
            {
                int index = Array.IndexOf(Ai, a);
                if (lowerindexbound > index)
                    lowerindexmap.Add(index);
            }

            foreach (var a in endPart)
            {
                int index = Array.IndexOf(Ai, a);
                if (upperindexbound < index)
                    upperindexmap.Add(index);
            }


            //List<int> remainderSubsequence = new List<int>();
            
            foreach (var a in middlePart)
            {
                int index = Array.IndexOf(Ai, a);
                if (lowerindexbound < index && index < upperindexbound)
                    middleindexmap.Add(index);
            }

            //Issue: Lowest Index has The highest "a" in either beginPart or endPart
            //Addresses here
            lowerindexmap.Sort();
            middleindexmap.Sort();
            upperindexmap.Sort();


            foreach (var i in middleindexmap)
            {
                int a = Ai[i];
                for (int j = 0; j < seq.Count; j++)
                {
                    // if it between j and j+1
                    if (a > seq[j] && j + 1 < seq.Count)
                    {
                        if (a < seq[j + 1])
                        {
                            seq.Insert(j + 1, a);
                            break;
                        }
                    }
                }

            }


            foreach (var i in upperindexmap)
            {
                int a = Ai[i];
                if (seq.Last() < a)
                {
                    seq.Add(a);
                }
            }


            lowerindexmap.Reverse();
            foreach (var i in lowerindexmap)
            {
                int a = Ai[i];
                if (seq.First() > a)
                {
                    seq.Insert(0, a);
                }

            }

            //// Check indies, must be in ascending order
            //List<int> seqIndexs = new List<int>();
            //foreach (var a in seq)
            //{
            //    int index = Array.IndexOf(Ai, a);
            //    seqIndexs.Add(index);
            //}

            // no duplicates
            //return seq.Union(remainderSubsequence).ToList();
            return seq;
        }

    }
}
