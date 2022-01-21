using System;
using System.Collections.Generic;
using System.Text;

//See: https://github.com/memo33/jDBPFX/blob/master/src/jdbpfx/DBPFTGI.java
namespace SC4DP2022_wpf {
	public class DBPFTGI {
		private readonly uint _type;
		public uint type {
			get { return _type; }
			//set { myVar = value; }
		}
		private readonly uint _group;
		public uint group {
			get { return _group; }
			//set { myVar = value; }
		}
		private readonly uint _instance;
		public uint instance {
			get { return _instance; }
			//set { myVar = value; }
		}
		private readonly uint _label;
		public uint label {
			get { return _label; }
			//set { _label = value; }
		}


		public static readonly DBPFTGI BLANKTGI; /** BLANKTGI (0, 0, 0) */
		public static readonly DBPFTGI DIRECTORY; /** Directory file (0xe86b1eef, 0xe86b1eef, 0x286b1f03) */
		public static readonly DBPFTGI LD; /** LD file (0x6be74c60, 0x6be74c60, -1) */
		public static readonly DBPFTGI EXEMPLAR_ROAD; /** Exemplar file: road network (0x6534284a, 0x2821ed93, -1) */
		public static readonly DBPFTGI EXEMPLAR_STREET; /** Exemplar file: street network (0x6534284a, 0xa92a02ea, -1) */
		public static readonly DBPFTGI EXEMPLAR_ONEWAYROAD; /** Exemplar file: one-way road network (0x6534284a, 0xcbe084cb, -1) */
		public static readonly DBPFTGI EXEMPLAR_AVENUE; /** Exemplar file: avenue network (0x6534284a, 0xcb730fac, -1) */
		public static readonly DBPFTGI EXEMPLAR_HIGHWAY; /** Exemplar file: elevated highway network (0x6534284a, 0xa8434037, -1) */
		public static readonly DBPFTGI EXEMPLAR_GROUNDHIGHWAY; /** Exemplar file: ground highway network (0x6534284a, 0xebe084d1, -1) */
		public static readonly DBPFTGI EXEMPLAR_DIRTROAD; /** Exemplar file: dirt road/ANT/RHW network (0x6534284a, 0x6be08658, -1) */
		public static readonly DBPFTGI EXEMPLAR_RAIL; /** Exemplar file: rail network (0x6534284a, 0xe8347989, -1) */
		public static readonly DBPFTGI EXEMPLAR_LIGHTRAIL; /** Exemplar file: light rail network (0x6534284a, 0x2b79dffb, -1) */
		public static readonly DBPFTGI EXEMPLAR_MONORAIL; /** Exemplar file: monorail network (0x6534284a, 0xebe084c2, -1) */
		public static readonly DBPFTGI EXEMPLAR_POWERPOLE; /** Exemplar file: power poles network (0x6534284a, 0x088e1962, -1) */
		public static readonly DBPFTGI EXEMPLAR_T21; /** Exemplar file: Type 21 (0x6534284a, 0x89AC5643, -1) */
		public static readonly DBPFTGI EXEMPLAR; /** Exemplar file: LotInfo, LotConfig (0x6534284a, -1, -1) */
		public static readonly DBPFTGI COHORT; /** Cohort file (0x05342861, -1, -1) */
		public static readonly DBPFTGI PNG_ICON; /** PNG file: Menu building icons, bridges, overlays (0x856ddbac, 0x6a386d26, -1) */
		public static readonly DBPFTGI PNG; /** PNG file (image, icon) (0x856ddbac, -1, -1) */
		[Obsolete("Use FISH_MISC instead.")]
		public static readonly DBPFTGI FSH_TRANSIT; /** FSH file: Transit Textures/Buildings/Bridges/Misc (0x7ab50e44, 0x1abe787d, -1) */
		public static readonly DBPFTGI FSH_MISC; /** FSH file: Transit Textures/Buildings/Bridges/Misc (0x7ab50e44, 0x1abe787d, -1) */
		public static readonly DBPFTGI FSH_BASE_OVERLAY; /** FSH file: Base and Overlay Lot Textures (0x7ab50e44, 0x0986135e, -1) */
		public static readonly DBPFTGI FSH_SHADOW; /** FSH file: Transit Network Shadows (Masks) (0x7ab50e44, 0x2BC2759a, -1) */
		public static readonly DBPFTGI FSH_ANIM_PROPS; /** FSH file: Animation Sprites (Props) (0x7ab50e44, 0x2a2458f9, -1) */
		public static readonly DBPFTGI FSH_ANIM_NONPROPS; /** FSH file: Animation Sprites (Non Props) (0x7ab50e44, 0x49a593e7, -1) */
		public static readonly DBPFTGI FSH_TERRAIN_FOUNDATION; /** FSH file: Terrain And Foundations (0x7ab50e44, 0x891b0e1a, -1) */
		public static readonly DBPFTGI FSH_UI; /** FSH file: User Interface Images (0x7ab50e44, 0x46a006b0, -1) */
		public static readonly DBPFTGI FSH; /** FSH file: Textures (0x7ab50e44, -1, -1) */
		public static readonly DBPFTGI S3D_MAXIS; /** S3D file: Maxis Models (0x5ad0e817, 0xbadb57f1, -1) */
		public static readonly DBPFTGI S3D; /** S3D file: Models (0x5ad0e817, -1, -1) */
		public static readonly DBPFTGI SC4PATH_2D; /** SC4PATH (2D) (0x296678f7, 0x69668828, -1) */
		public static readonly DBPFTGI SC4PATH_3D; /** SC4PATH (3D) (0x296678f7, 0xa966883f, -1) */
		public static readonly DBPFTGI SC4PATH; /** SC4PATH file (0x296678f7, -1, -1) */
		public static readonly DBPFTGI LUA; /** LUA file: Missions, Advisors, Tutorials and Packaging files (0xca63e2a3, 0x4a5e8ef6, -1) */
		public static readonly DBPFTGI LUA_GEN; /** LUA file: Generators, Attractors, Repulsors and System LUA (0xca63e2a3, 0x4a5e8f3f, -1) */
		public static readonly DBPFTGI RUL; /** RUL file: Network rules (0x0a5bcf4b, 0xaa5bcf57, -1) */
		public static readonly DBPFTGI WAV; /** WAV file (0x2026960b, 0xaa4d1933, -1) */
		public static readonly DBPFTGI LTEXT; /** LTEXT or WAV file (0x2026960b, -1, -1) */
		public static readonly DBPFTGI EFFDIR; /** Effect Directory file (0xea5118b0, -1, -1) */
		public static readonly DBPFTGI INI_FONT; /** Font Table INI (0, 0x4a87bfe8, 0x2a87bffc) */
		public static readonly DBPFTGI INI_NETWORK; /** Network INI: Remapping, Bridge Exemplars (0, 0x8a5971c5, 0x8a5993b9) */
		public static readonly DBPFTGI INI; /** INI file (0, 0x8a5971c5, -1) */
		public static readonly DBPFTGI NULLTGI; /** NULLTGI (-1, -1, -1) */

		public DBPFTGI(uint type, uint group, uint instance) {
			_type = type;
			_group = group;
			_instance = instance;
		}


		public string GetLabel() {
			return null;
		}

		public bool IsTypeZero() {
			return this.type == 0;
		}
		public bool IsGroupZero() {
			return this.group == 0;
		}
		public bool IsInstanceZero() {
			return this.instance == 0;
		}








		//Regarding the code below, I'm not sure what to do with it. this is how memo implemented it, but I'm not entirely sure how it works or why it works this way. can it be better done as a simple dictionary instead? i think all he's trying to do is create specific types of DBPFTGI with the given values, so would it not be simpler to add them into a dictionary and throw the dictionary in the constructor? i think it would accomplish the same thing and it makes more sense to me....but i need to study how these are used more closely to be sure that would work.
		
		////This static constructor will be called as soon as the class is loaded into memory, and not necessarily when an object is created
		//static DBPFTGI() { 
		//	 //Masks need to be ordered "bottom-up", that is, specialized entries need to be inserted first, more general ones later.
		//	BLANKTGI = new TGILookup(0, 0, 0, "-");
		//	DIRECTORY = new TGILookup(0xe86b1eef, 0xe86b1eef, 0x286b1f03, "DIR");
		//	LD = new TGILookup(0x6be74c60, 0x6be74c60, 0, "LD");
		//	S3D_MAXIS = new TGILookup(0x5ad0e817, 0xbadb57f1, 0, "S3D");
		//	S3D = new TGILookup(0x5ad0e817, 0, 0, "S3D");
		//	COHORT = new TGILookup(0x05342861, 0, 0, "COHORT");
		//	EXEMPLAR_ROAD = new TGILookup(0x6534284a, 0x2821ed93, 0, "EXEMPLAR (Road)");
		//	EXEMPLAR_STREET = new TGILookup(0x6534284a, 0xa92a02ea, 0, "EXEMPLAR (Street)");
		//	EXEMPLAR_ONEWAYROAD = new TGILookup(0x6534284a, 0xcbe084cb, 0, "EXEMPLAR (One-Way Road)");
		//	EXEMPLAR_AVENUE = new TGILookup(0x6534284a, 0xcb730fac, 0, "EXEMPLAR (Avenue)");
		//	EXEMPLAR_HIGHWAY = new TGILookup(0x6534284a, 0xa8434037, 0, "EXEMPLAR (Highway)");
		//	EXEMPLAR_GROUNDHIGHWAY = new TGILookup(0x6534284a, 0xebe084d1, 0, "EXEMPLAR (Ground Highway)");
		//	EXEMPLAR_DIRTROAD = new TGILookup(0x6534284a, 0x6be08658, 0, "EXEMPLAR (Dirtroad)");
		//	EXEMPLAR_RAIL = new TGILookup(0x6534284a, 0xe8347989, 0, "EXEMPLAR (Rail)");
		//	EXEMPLAR_LIGHTRAIL = new TGILookup(0x6534284a, 0x2b79dffb, 0, "EXEMPLAR (Lightrail)");
		//	EXEMPLAR_MONORAIL = new TGILookup(0x6534284a, 0xebe084c2, 0, "EXEMPLAR (Monorail)");
		//	EXEMPLAR_POWERPOLE = new TGILookup(0x6534284a, 0x088e1962, 0, "EXEMPLAR (Power Pole)");
		//	EXEMPLAR_T21 = new TGILookup(0x6534284a, 0x89ac5643, 0, "EXEMPLAR (T21)");
		//	EXEMPLAR = new TGILookup(0x6534284a, 0, 0, "EXEMPLAR");
		//	FSH_MISC = new TGILookup(0x7ab50e44, 0x1abe787d, 0, "FSH (Misc)");
		//	FSH_TRANSIT = FSH_MISC;
		//	FSH_BASE_OVERLAY = new TGILookup(0x7ab50e44, 0x0986135e, 0, "FSH (Base/Overlay Texture)");
		//	FSH_SHADOW = new TGILookup(0x7ab50e44, 0x2BC2759a, 0, "FSH (Shadow TGILookup)");
		//	FSH_ANIM_PROPS = new TGILookup(0x7ab50e44, 0x2a2458f9, 0, "FSH (Animation Sprites (Props))");
		//	FSH_ANIM_NONPROPS = new TGILookup(0x7ab50e44, 0x49a593e7, 0, "FSH (Animation Sprites (Non Props))");
		//	FSH_TERRAIN_FOUNDATION = new TGILookup(0x7ab50e44, 0x891b0e1a, 0, "FSH (Terrain/Foundation)");
		//	FSH_UI = new TGILookup(0x7ab50e44, 0x46a006b0, 0, "FSH (UI Image)");
		//	FSH = new TGILookup(0x7ab50e44, 0, 0, "FSH");
		//	SC4PATH_2D = new TGILookup(0x296678f7, 0x69668828, 0, "SC4PATH (2D)");
		//	SC4PATH_3D = new TGILookup(0x296678f7, 0xa966883f, 0, "SC4PATH (3D)");
		//	SC4PATH = new TGILookup(0x296678f7, 0, 0, "SC4PATH");
		//	PNG_ICON = new TGILookup(0x856ddbac, 0x6a386d26, 0, "PNG (Icon)");
		//	PNG = new TGILookup(0x856ddbac, 0, 0, "PNG");
		//	LUA = new TGILookup(0xca63e2a3, 0x4a5e8ef6, 0, "LUA");
		//	LUA_GEN = new TGILookup(0xca63e2a3, 0x4a5e8f3f, 0, "LUA (Generators)");
		//	WAV = new TGILookup(0x2026960b, 0xaa4d1933, 0, "WAV");
		//	LTEXT = new TGILookup(0x2026960b, 0, 0, "LTEXT");
		//	INI_FONT = new TGILookup(0, 0x4a87bfe8, 0x2a87bffc, "INI (Font Table)");
		//	INI_NETWORK = new TGILookup(0, 0x8a5971c5, 0x8a5993b9, "INI (Networks)");
		//	INI = new TGILookup(0, 0x8a5971c5, 0, "INI");
		//	RUL = new TGILookup(0x0a5bcf4b, 0xaa5bcf57, 0, "RUL");
		//	EFFDIR = new TGILookup(0xea5118b0, 0, 0, "EFFDIR");
		//	NULLTGI = new TGILookup(0, 0, 0, "UNKNOWN"); // any TGI matches this last one
		//}

		//private class TGILookup : DBPFTGI {
		//	private string label;
		//	public readonly Queue<TGILookup> values = new Queue<TGILookup>(); //Use a queue for ordered iteration

		//	public TGILookup(uint type, uint group, uint instance, string label) : base(type, group, instance) {
		//		this.label = label;
		//		values.Enqueue(this);
		//	}
		//}
	}

}
