﻿#pragma warning disable 0219 // variable assigned but not used.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StringTest : MonoBehaviour {
	
	const uint DATA_COUNT = 1000;
    string[] m_data;
	string m_stringFormat;
	
	void ConcatString() {
        string result = "";
		for (int i =0;i<DATA_COUNT;++i) {
            result += (m_data[i]);
		}
	}
	
	void StringFormat() {
			
		string result = string.Format (m_stringFormat,
			m_data[0],
			m_data[1],
			m_data[2],
			m_data[3],
			m_data[4],
			m_data[5],
			m_data[6],
			m_data[7],
			m_data[8],
			m_data[9],
			m_data[10],
			m_data[11],
			m_data[12],
			m_data[13],
			m_data[14],
			m_data[15],
			m_data[16],
			m_data[17],
			m_data[18],
			m_data[19],
			m_data[20],
			m_data[21],
			m_data[22],
			m_data[23],
			m_data[24],
			m_data[25],
			m_data[26],
			m_data[27],
			m_data[28],
			m_data[29],
			m_data[30],
			m_data[31],
			m_data[32],
			m_data[33],
			m_data[34],
			m_data[35],
			m_data[36],
			m_data[37],
			m_data[38],
			m_data[39],
			m_data[40],
			m_data[41],
			m_data[42],
			m_data[43],
			m_data[44],
			m_data[45],
			m_data[46],
			m_data[47],
			m_data[48],
			m_data[49],
			m_data[50],
			m_data[51],
			m_data[52],
			m_data[53],
			m_data[54],
			m_data[55],
			m_data[56],
			m_data[57],
			m_data[58],
			m_data[59],
			m_data[60],
			m_data[61],
			m_data[62],
			m_data[63],
			m_data[64],
			m_data[65],
			m_data[66],
			m_data[67],
			m_data[68],
			m_data[69],
			m_data[70],
			m_data[71],
			m_data[72],
			m_data[73],
			m_data[74],
			m_data[75],
			m_data[76],
			m_data[77],
			m_data[78],
			m_data[79],
			m_data[80],
			m_data[81],
			m_data[82],
			m_data[83],
			m_data[84],
			m_data[85],
			m_data[86],
			m_data[87],
			m_data[88],
			m_data[89],
			m_data[90],
			m_data[91],
			m_data[92],
			m_data[93],
			m_data[94],
			m_data[95],
			m_data[96],
			m_data[97],
			m_data[98],
			m_data[99],
			m_data[100],
			m_data[101],
			m_data[102],
			m_data[103],
			m_data[104],
			m_data[105],
			m_data[106],
			m_data[107],
			m_data[108],
			m_data[109],
			m_data[110],
			m_data[111],
			m_data[112],
			m_data[113],
			m_data[114],
			m_data[115],
			m_data[116],
			m_data[117],
			m_data[118],
			m_data[119],
			m_data[120],
			m_data[121],
			m_data[122],
			m_data[123],
			m_data[124],
			m_data[125],
			m_data[126],
			m_data[127],
			m_data[128],
			m_data[129],
			m_data[130],
			m_data[131],
			m_data[132],
			m_data[133],
			m_data[134],
			m_data[135],
			m_data[136],
			m_data[137],
			m_data[138],
			m_data[139],
			m_data[140],
			m_data[141],
			m_data[142],
			m_data[143],
			m_data[144],
			m_data[145],
			m_data[146],
			m_data[147],
			m_data[148],
			m_data[149],
			m_data[150],
			m_data[151],
			m_data[152],
			m_data[153],
			m_data[154],
			m_data[155],
			m_data[156],
			m_data[157],
			m_data[158],
			m_data[159],
			m_data[160],
			m_data[161],
			m_data[162],
			m_data[163],
			m_data[164],
			m_data[165],
			m_data[166],
			m_data[167],
			m_data[168],
			m_data[169],
			m_data[170],
			m_data[171],
			m_data[172],
			m_data[173],
			m_data[174],
			m_data[175],
			m_data[176],
			m_data[177],
			m_data[178],
			m_data[179],
			m_data[180],
			m_data[181],
			m_data[182],
			m_data[183],
			m_data[184],
			m_data[185],
			m_data[186],
			m_data[187],
			m_data[188],
			m_data[189],
			m_data[190],
			m_data[191],
			m_data[192],
			m_data[193],
			m_data[194],
			m_data[195],
			m_data[196],
			m_data[197],
			m_data[198],
			m_data[199],
			m_data[200],
			m_data[201],
			m_data[202],
			m_data[203],
			m_data[204],
			m_data[205],
			m_data[206],
			m_data[207],
			m_data[208],
			m_data[209],
			m_data[210],
			m_data[211],
			m_data[212],
			m_data[213],
			m_data[214],
			m_data[215],
			m_data[216],
			m_data[217],
			m_data[218],
			m_data[219],
			m_data[220],
			m_data[221],
			m_data[222],
			m_data[223],
			m_data[224],
			m_data[225],
			m_data[226],
			m_data[227],
			m_data[228],
			m_data[229],
			m_data[230],
			m_data[231],
			m_data[232],
			m_data[233],
			m_data[234],
			m_data[235],
			m_data[236],
			m_data[237],
			m_data[238],
			m_data[239],
			m_data[240],
			m_data[241],
			m_data[242],
			m_data[243],
			m_data[244],
			m_data[245],
			m_data[246],
			m_data[247],
			m_data[248],
			m_data[249],
			m_data[250],
			m_data[251],
			m_data[252],
			m_data[253],
			m_data[254],
			m_data[255],
			m_data[256],
			m_data[257],
			m_data[258],
			m_data[259],
			m_data[260],
			m_data[261],
			m_data[262],
			m_data[263],
			m_data[264],
			m_data[265],
			m_data[266],
			m_data[267],
			m_data[268],
			m_data[269],
			m_data[270],
			m_data[271],
			m_data[272],
			m_data[273],
			m_data[274],
			m_data[275],
			m_data[276],
			m_data[277],
			m_data[278],
			m_data[279],
			m_data[280],
			m_data[281],
			m_data[282],
			m_data[283],
			m_data[284],
			m_data[285],
			m_data[286],
			m_data[287],
			m_data[288],
			m_data[289],
			m_data[290],
			m_data[291],
			m_data[292],
			m_data[293],
			m_data[294],
			m_data[295],
			m_data[296],
			m_data[297],
			m_data[298],
			m_data[299],
			m_data[300],
			m_data[301],
			m_data[302],
			m_data[303],
			m_data[304],
			m_data[305],
			m_data[306],
			m_data[307],
			m_data[308],
			m_data[309],
			m_data[310],
			m_data[311],
			m_data[312],
			m_data[313],
			m_data[314],
			m_data[315],
			m_data[316],
			m_data[317],
			m_data[318],
			m_data[319],
			m_data[320],
			m_data[321],
			m_data[322],
			m_data[323],
			m_data[324],
			m_data[325],
			m_data[326],
			m_data[327],
			m_data[328],
			m_data[329],
			m_data[330],
			m_data[331],
			m_data[332],
			m_data[333],
			m_data[334],
			m_data[335],
			m_data[336],
			m_data[337],
			m_data[338],
			m_data[339],
			m_data[340],
			m_data[341],
			m_data[342],
			m_data[343],
			m_data[344],
			m_data[345],
			m_data[346],
			m_data[347],
			m_data[348],
			m_data[349],
			m_data[350],
			m_data[351],
			m_data[352],
			m_data[353],
			m_data[354],
			m_data[355],
			m_data[356],
			m_data[357],
			m_data[358],
			m_data[359],
			m_data[360],
			m_data[361],
			m_data[362],
			m_data[363],
			m_data[364],
			m_data[365],
			m_data[366],
			m_data[367],
			m_data[368],
			m_data[369],
			m_data[370],
			m_data[371],
			m_data[372],
			m_data[373],
			m_data[374],
			m_data[375],
			m_data[376],
			m_data[377],
			m_data[378],
			m_data[379],
			m_data[380],
			m_data[381],
			m_data[382],
			m_data[383],
			m_data[384],
			m_data[385],
			m_data[386],
			m_data[387],
			m_data[388],
			m_data[389],
			m_data[390],
			m_data[391],
			m_data[392],
			m_data[393],
			m_data[394],
			m_data[395],
			m_data[396],
			m_data[397],
			m_data[398],
			m_data[399],
			m_data[400],
			m_data[401],
			m_data[402],
			m_data[403],
			m_data[404],
			m_data[405],
			m_data[406],
			m_data[407],
			m_data[408],
			m_data[409],
			m_data[410],
			m_data[411],
			m_data[412],
			m_data[413],
			m_data[414],
			m_data[415],
			m_data[416],
			m_data[417],
			m_data[418],
			m_data[419],
			m_data[420],
			m_data[421],
			m_data[422],
			m_data[423],
			m_data[424],
			m_data[425],
			m_data[426],
			m_data[427],
			m_data[428],
			m_data[429],
			m_data[430],
			m_data[431],
			m_data[432],
			m_data[433],
			m_data[434],
			m_data[435],
			m_data[436],
			m_data[437],
			m_data[438],
			m_data[439],
			m_data[440],
			m_data[441],
			m_data[442],
			m_data[443],
			m_data[444],
			m_data[445],
			m_data[446],
			m_data[447],
			m_data[448],
			m_data[449],
			m_data[450],
			m_data[451],
			m_data[452],
			m_data[453],
			m_data[454],
			m_data[455],
			m_data[456],
			m_data[457],
			m_data[458],
			m_data[459],
			m_data[460],
			m_data[461],
			m_data[462],
			m_data[463],
			m_data[464],
			m_data[465],
			m_data[466],
			m_data[467],
			m_data[468],
			m_data[469],
			m_data[470],
			m_data[471],
			m_data[472],
			m_data[473],
			m_data[474],
			m_data[475],
			m_data[476],
			m_data[477],
			m_data[478],
			m_data[479],
			m_data[480],
			m_data[481],
			m_data[482],
			m_data[483],
			m_data[484],
			m_data[485],
			m_data[486],
			m_data[487],
			m_data[488],
			m_data[489],
			m_data[490],
			m_data[491],
			m_data[492],
			m_data[493],
			m_data[494],
			m_data[495],
			m_data[496],
			m_data[497],
			m_data[498],
			m_data[499],
			m_data[500],
			m_data[501],
			m_data[502],
			m_data[503],
			m_data[504],
			m_data[505],
			m_data[506],
			m_data[507],
			m_data[508],
			m_data[509],
			m_data[510],
			m_data[511],
			m_data[512],
			m_data[513],
			m_data[514],
			m_data[515],
			m_data[516],
			m_data[517],
			m_data[518],
			m_data[519],
			m_data[520],
			m_data[521],
			m_data[522],
			m_data[523],
			m_data[524],
			m_data[525],
			m_data[526],
			m_data[527],
			m_data[528],
			m_data[529],
			m_data[530],
			m_data[531],
			m_data[532],
			m_data[533],
			m_data[534],
			m_data[535],
			m_data[536],
			m_data[537],
			m_data[538],
			m_data[539],
			m_data[540],
			m_data[541],
			m_data[542],
			m_data[543],
			m_data[544],
			m_data[545],
			m_data[546],
			m_data[547],
			m_data[548],
			m_data[549],
			m_data[550],
			m_data[551],
			m_data[552],
			m_data[553],
			m_data[554],
			m_data[555],
			m_data[556],
			m_data[557],
			m_data[558],
			m_data[559],
			m_data[560],
			m_data[561],
			m_data[562],
			m_data[563],
			m_data[564],
			m_data[565],
			m_data[566],
			m_data[567],
			m_data[568],
			m_data[569],
			m_data[570],
			m_data[571],
			m_data[572],
			m_data[573],
			m_data[574],
			m_data[575],
			m_data[576],
			m_data[577],
			m_data[578],
			m_data[579],
			m_data[580],
			m_data[581],
			m_data[582],
			m_data[583],
			m_data[584],
			m_data[585],
			m_data[586],
			m_data[587],
			m_data[588],
			m_data[589],
			m_data[590],
			m_data[591],
			m_data[592],
			m_data[593],
			m_data[594],
			m_data[595],
			m_data[596],
			m_data[597],
			m_data[598],
			m_data[599],
			m_data[600],
			m_data[601],
			m_data[602],
			m_data[603],
			m_data[604],
			m_data[605],
			m_data[606],
			m_data[607],
			m_data[608],
			m_data[609],
			m_data[610],
			m_data[611],
			m_data[612],
			m_data[613],
			m_data[614],
			m_data[615],
			m_data[616],
			m_data[617],
			m_data[618],
			m_data[619],
			m_data[620],
			m_data[621],
			m_data[622],
			m_data[623],
			m_data[624],
			m_data[625],
			m_data[626],
			m_data[627],
			m_data[628],
			m_data[629],
			m_data[630],
			m_data[631],
			m_data[632],
			m_data[633],
			m_data[634],
			m_data[635],
			m_data[636],
			m_data[637],
			m_data[638],
			m_data[639],
			m_data[640],
			m_data[641],
			m_data[642],
			m_data[643],
			m_data[644],
			m_data[645],
			m_data[646],
			m_data[647],
			m_data[648],
			m_data[649],
			m_data[650],
			m_data[651],
			m_data[652],
			m_data[653],
			m_data[654],
			m_data[655],
			m_data[656],
			m_data[657],
			m_data[658],
			m_data[659],
			m_data[660],
			m_data[661],
			m_data[662],
			m_data[663],
			m_data[664],
			m_data[665],
			m_data[666],
			m_data[667],
			m_data[668],
			m_data[669],
			m_data[670],
			m_data[671],
			m_data[672],
			m_data[673],
			m_data[674],
			m_data[675],
			m_data[676],
			m_data[677],
			m_data[678],
			m_data[679],
			m_data[680],
			m_data[681],
			m_data[682],
			m_data[683],
			m_data[684],
			m_data[685],
			m_data[686],
			m_data[687],
			m_data[688],
			m_data[689],
			m_data[690],
			m_data[691],
			m_data[692],
			m_data[693],
			m_data[694],
			m_data[695],
			m_data[696],
			m_data[697],
			m_data[698],
			m_data[699],
			m_data[700],
			m_data[701],
			m_data[702],
			m_data[703],
			m_data[704],
			m_data[705],
			m_data[706],
			m_data[707],
			m_data[708],
			m_data[709],
			m_data[710],
			m_data[711],
			m_data[712],
			m_data[713],
			m_data[714],
			m_data[715],
			m_data[716],
			m_data[717],
			m_data[718],
			m_data[719],
			m_data[720],
			m_data[721],
			m_data[722],
			m_data[723],
			m_data[724],
			m_data[725],
			m_data[726],
			m_data[727],
			m_data[728],
			m_data[729],
			m_data[730],
			m_data[731],
			m_data[732],
			m_data[733],
			m_data[734],
			m_data[735],
			m_data[736],
			m_data[737],
			m_data[738],
			m_data[739],
			m_data[740],
			m_data[741],
			m_data[742],
			m_data[743],
			m_data[744],
			m_data[745],
			m_data[746],
			m_data[747],
			m_data[748],
			m_data[749],
			m_data[750],
			m_data[751],
			m_data[752],
			m_data[753],
			m_data[754],
			m_data[755],
			m_data[756],
			m_data[757],
			m_data[758],
			m_data[759],
			m_data[760],
			m_data[761],
			m_data[762],
			m_data[763],
			m_data[764],
			m_data[765],
			m_data[766],
			m_data[767],
			m_data[768],
			m_data[769],
			m_data[770],
			m_data[771],
			m_data[772],
			m_data[773],
			m_data[774],
			m_data[775],
			m_data[776],
			m_data[777],
			m_data[778],
			m_data[779],
			m_data[780],
			m_data[781],
			m_data[782],
			m_data[783],
			m_data[784],
			m_data[785],
			m_data[786],
			m_data[787],
			m_data[788],
			m_data[789],
			m_data[790],
			m_data[791],
			m_data[792],
			m_data[793],
			m_data[794],
			m_data[795],
			m_data[796],
			m_data[797],
			m_data[798],
			m_data[799],
			m_data[800],
			m_data[801],
			m_data[802],
			m_data[803],
			m_data[804],
			m_data[805],
			m_data[806],
			m_data[807],
			m_data[808],
			m_data[809],
			m_data[810],
			m_data[811],
			m_data[812],
			m_data[813],
			m_data[814],
			m_data[815],
			m_data[816],
			m_data[817],
			m_data[818],
			m_data[819],
			m_data[820],
			m_data[821],
			m_data[822],
			m_data[823],
			m_data[824],
			m_data[825],
			m_data[826],
			m_data[827],
			m_data[828],
			m_data[829],
			m_data[830],
			m_data[831],
			m_data[832],
			m_data[833],
			m_data[834],
			m_data[835],
			m_data[836],
			m_data[837],
			m_data[838],
			m_data[839],
			m_data[840],
			m_data[841],
			m_data[842],
			m_data[843],
			m_data[844],
			m_data[845],
			m_data[846],
			m_data[847],
			m_data[848],
			m_data[849],
			m_data[850],
			m_data[851],
			m_data[852],
			m_data[853],
			m_data[854],
			m_data[855],
			m_data[856],
			m_data[857],
			m_data[858],
			m_data[859],
			m_data[860],
			m_data[861],
			m_data[862],
			m_data[863],
			m_data[864],
			m_data[865],
			m_data[866],
			m_data[867],
			m_data[868],
			m_data[869],
			m_data[870],
			m_data[871],
			m_data[872],
			m_data[873],
			m_data[874],
			m_data[875],
			m_data[876],
			m_data[877],
			m_data[878],
			m_data[879],
			m_data[880],
			m_data[881],
			m_data[882],
			m_data[883],
			m_data[884],
			m_data[885],
			m_data[886],
			m_data[887],
			m_data[888],
			m_data[889],
			m_data[890],
			m_data[891],
			m_data[892],
			m_data[893],
			m_data[894],
			m_data[895],
			m_data[896],
			m_data[897],
			m_data[898],
			m_data[899],
			m_data[900],
			m_data[901],
			m_data[902],
			m_data[903],
			m_data[904],
			m_data[905],
			m_data[906],
			m_data[907],
			m_data[908],
			m_data[909],
			m_data[910],
			m_data[911],
			m_data[912],
			m_data[913],
			m_data[914],
			m_data[915],
			m_data[916],
			m_data[917],
			m_data[918],
			m_data[919],
			m_data[920],
			m_data[921],
			m_data[922],
			m_data[923],
			m_data[924],
			m_data[925],
			m_data[926],
			m_data[927],
			m_data[928],
			m_data[929],
			m_data[930],
			m_data[931],
			m_data[932],
			m_data[933],
			m_data[934],
			m_data[935],
			m_data[936],
			m_data[937],
			m_data[938],
			m_data[939],
			m_data[940],
			m_data[941],
			m_data[942],
			m_data[943],
			m_data[944],
			m_data[945],
			m_data[946],
			m_data[947],
			m_data[948],
			m_data[949],
			m_data[950],
			m_data[951],
			m_data[952],
			m_data[953],
			m_data[954],
			m_data[955],
			m_data[956],
			m_data[957],
			m_data[958],
			m_data[959],
			m_data[960],
			m_data[961],
			m_data[962],
			m_data[963],
			m_data[964],
			m_data[965],
			m_data[966],
			m_data[967],
			m_data[968],
			m_data[969],
			m_data[970],
			m_data[971],
			m_data[972],
			m_data[973],
			m_data[974],
			m_data[975],
			m_data[976],
			m_data[977],
			m_data[978],
			m_data[979],
			m_data[980],
			m_data[981],
			m_data[982],
			m_data[983],
			m_data[984],
			m_data[985],
			m_data[986],
			m_data[987],
			m_data[988],
			m_data[989],
			m_data[990],
			m_data[991],
			m_data[992],
			m_data[993],
			m_data[994],
			m_data[995],
			m_data[996],
			m_data[997],
			m_data[998],
			m_data[999]					
		);	
	}
	
	void StringBuilder() {
		
		System.Text.StringBuilder result = new System.Text.StringBuilder("");
	
		for (int i =0;i<DATA_COUNT;++i) {
			result.Append(m_data[i]);
		}
	}
	
	void Awake() {
		m_data = new string[DATA_COUNT];
		System.Text.StringBuilder string_builder = new System.Text.StringBuilder("");
		for (uint i=0;i<DATA_COUNT;++i) {
			m_data[i] = ((int)(Random.value * 9)).ToString();			
			string_builder.Append("{");
			string_builder.Append(i.ToString());
			string_builder.Append("}");
			string_builder.Append(" ");			
		}
		
		m_stringFormat = string_builder.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		Profiler.BeginSample("ConcatString");
		ConcatString ();
		Profiler.EndSample();

		Profiler.BeginSample("StringFormat");
		StringFormat();
		Profiler.EndSample();

		Profiler.BeginSample("StringBuilder");
		StringBuilder();
		Profiler.EndSample();
	}
}
