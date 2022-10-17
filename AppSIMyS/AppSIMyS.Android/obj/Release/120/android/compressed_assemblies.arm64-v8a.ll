; ModuleID = 'obj\Release\120\android\compressed_assemblies.arm64-v8a.ll'
source_filename = "obj\Release\120\android\compressed_assemblies.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android"


%struct.CompressedAssemblyDescriptor = type {
	i32,; uint32_t uncompressed_file_size
	i8,; bool loaded
	i8*; uint8_t* data
}

%struct.CompressedAssemblies = type {
	i32,; uint32_t count
	%struct.CompressedAssemblyDescriptor*; CompressedAssemblyDescriptor* descriptors
}
@__CompressedAssemblyDescriptor_data_0 = internal global [520192 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_1 = internal global [236032 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_2 = internal global [2885632 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_3 = internal global [16384 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_4 = internal global [82832 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_5 = internal global [49584 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_6 = internal global [166912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_7 = internal global [11776 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_8 = internal global [34816 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_9 = internal global [66048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_10 = internal global [48640 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_11 = internal global [43008 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_12 = internal global [33280 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_13 = internal global [45568 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_14 = internal global [32768 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_15 = internal global [2255360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_16 = internal global [122880 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_17 = internal global [606720 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_18 = internal global [684544 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_19 = internal global [9216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_20 = internal global [65536 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_21 = internal global [21504 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_22 = internal global [100352 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_23 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_24 = internal global [46080 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_25 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_26 = internal global [35328 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_27 = internal global [40504 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_28 = internal global [36352 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_29 = internal global [455168 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_30 = internal global [50688 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_31 = internal global [4596736 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_32 = internal global [14728 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_33 = internal global [424960 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_34 = internal global [1370624 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_35 = internal global [73216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_36 = internal global [29696 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_37 = internal global [78848 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_38 = internal global [219648 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_39 = internal global [38912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_40 = internal global [7680 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_41 = internal global [5632 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_42 = internal global [419328 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_43 = internal global [55808 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_44 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_45 = internal global [11776 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_46 = internal global [79360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_47 = internal global [1471488 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_48 = internal global [981504 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_49 = internal global [53248 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_50 = internal global [16896 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_51 = internal global [509952 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_52 = internal global [30720 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_53 = internal global [17920 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_54 = internal global [79360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_55 = internal global [598528 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_56 = internal global [25088 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_57 = internal global [9216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_58 = internal global [44032 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_59 = internal global [175104 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_60 = internal global [15872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_61 = internal global [15360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_62 = internal global [16384 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_63 = internal global [17408 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_64 = internal global [36864 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_65 = internal global [424448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_66 = internal global [13312 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_67 = internal global [40448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_68 = internal global [57856 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_69 = internal global [37888 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_70 = internal global [1209344 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_71 = internal global [961536 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_72 = internal global [264104 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_73 = internal global [103424 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_74 = internal global [258048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_75 = internal global [18072 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_76 = internal global [154112 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_77 = internal global [112128 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_78 = internal global [122368 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_79 = internal global [1539072 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_80 = internal global [912384 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_81 = internal global [373248 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_82 = internal global [89600 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_83 = internal global [98816 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_84 = internal global [386560 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_85 = internal global [125440 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_86 = internal global [2208256 x i8] zeroinitializer, align 1


; Compressed assembly data storage
@compressed_assembly_descriptors = internal global [87 x %struct.CompressedAssemblyDescriptor] [
	; 0
	%struct.CompressedAssemblyDescriptor {
		i32 520192, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([520192 x i8], [520192 x i8]* @__CompressedAssemblyDescriptor_data_0, i32 0, i32 0); data
	}, 
	; 1
	%struct.CompressedAssemblyDescriptor {
		i32 236032, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([236032 x i8], [236032 x i8]* @__CompressedAssemblyDescriptor_data_1, i32 0, i32 0); data
	}, 
	; 2
	%struct.CompressedAssemblyDescriptor {
		i32 2885632, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2885632 x i8], [2885632 x i8]* @__CompressedAssemblyDescriptor_data_2, i32 0, i32 0); data
	}, 
	; 3
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16384 x i8], [16384 x i8]* @__CompressedAssemblyDescriptor_data_3, i32 0, i32 0); data
	}, 
	; 4
	%struct.CompressedAssemblyDescriptor {
		i32 82832, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([82832 x i8], [82832 x i8]* @__CompressedAssemblyDescriptor_data_4, i32 0, i32 0); data
	}, 
	; 5
	%struct.CompressedAssemblyDescriptor {
		i32 49584, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([49584 x i8], [49584 x i8]* @__CompressedAssemblyDescriptor_data_5, i32 0, i32 0); data
	}, 
	; 6
	%struct.CompressedAssemblyDescriptor {
		i32 166912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([166912 x i8], [166912 x i8]* @__CompressedAssemblyDescriptor_data_6, i32 0, i32 0); data
	}, 
	; 7
	%struct.CompressedAssemblyDescriptor {
		i32 11776, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([11776 x i8], [11776 x i8]* @__CompressedAssemblyDescriptor_data_7, i32 0, i32 0); data
	}, 
	; 8
	%struct.CompressedAssemblyDescriptor {
		i32 34816, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([34816 x i8], [34816 x i8]* @__CompressedAssemblyDescriptor_data_8, i32 0, i32 0); data
	}, 
	; 9
	%struct.CompressedAssemblyDescriptor {
		i32 66048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([66048 x i8], [66048 x i8]* @__CompressedAssemblyDescriptor_data_9, i32 0, i32 0); data
	}, 
	; 10
	%struct.CompressedAssemblyDescriptor {
		i32 48640, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([48640 x i8], [48640 x i8]* @__CompressedAssemblyDescriptor_data_10, i32 0, i32 0); data
	}, 
	; 11
	%struct.CompressedAssemblyDescriptor {
		i32 43008, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([43008 x i8], [43008 x i8]* @__CompressedAssemblyDescriptor_data_11, i32 0, i32 0); data
	}, 
	; 12
	%struct.CompressedAssemblyDescriptor {
		i32 33280, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([33280 x i8], [33280 x i8]* @__CompressedAssemblyDescriptor_data_12, i32 0, i32 0); data
	}, 
	; 13
	%struct.CompressedAssemblyDescriptor {
		i32 45568, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([45568 x i8], [45568 x i8]* @__CompressedAssemblyDescriptor_data_13, i32 0, i32 0); data
	}, 
	; 14
	%struct.CompressedAssemblyDescriptor {
		i32 32768, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([32768 x i8], [32768 x i8]* @__CompressedAssemblyDescriptor_data_14, i32 0, i32 0); data
	}, 
	; 15
	%struct.CompressedAssemblyDescriptor {
		i32 2255360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2255360 x i8], [2255360 x i8]* @__CompressedAssemblyDescriptor_data_15, i32 0, i32 0); data
	}, 
	; 16
	%struct.CompressedAssemblyDescriptor {
		i32 122880, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([122880 x i8], [122880 x i8]* @__CompressedAssemblyDescriptor_data_16, i32 0, i32 0); data
	}, 
	; 17
	%struct.CompressedAssemblyDescriptor {
		i32 606720, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([606720 x i8], [606720 x i8]* @__CompressedAssemblyDescriptor_data_17, i32 0, i32 0); data
	}, 
	; 18
	%struct.CompressedAssemblyDescriptor {
		i32 684544, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([684544 x i8], [684544 x i8]* @__CompressedAssemblyDescriptor_data_18, i32 0, i32 0); data
	}, 
	; 19
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([9216 x i8], [9216 x i8]* @__CompressedAssemblyDescriptor_data_19, i32 0, i32 0); data
	}, 
	; 20
	%struct.CompressedAssemblyDescriptor {
		i32 65536, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([65536 x i8], [65536 x i8]* @__CompressedAssemblyDescriptor_data_20, i32 0, i32 0); data
	}, 
	; 21
	%struct.CompressedAssemblyDescriptor {
		i32 21504, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([21504 x i8], [21504 x i8]* @__CompressedAssemblyDescriptor_data_21, i32 0, i32 0); data
	}, 
	; 22
	%struct.CompressedAssemblyDescriptor {
		i32 100352, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([100352 x i8], [100352 x i8]* @__CompressedAssemblyDescriptor_data_22, i32 0, i32 0); data
	}, 
	; 23
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_23, i32 0, i32 0); data
	}, 
	; 24
	%struct.CompressedAssemblyDescriptor {
		i32 46080, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([46080 x i8], [46080 x i8]* @__CompressedAssemblyDescriptor_data_24, i32 0, i32 0); data
	}, 
	; 25
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_25, i32 0, i32 0); data
	}, 
	; 26
	%struct.CompressedAssemblyDescriptor {
		i32 35328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([35328 x i8], [35328 x i8]* @__CompressedAssemblyDescriptor_data_26, i32 0, i32 0); data
	}, 
	; 27
	%struct.CompressedAssemblyDescriptor {
		i32 40504, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([40504 x i8], [40504 x i8]* @__CompressedAssemblyDescriptor_data_27, i32 0, i32 0); data
	}, 
	; 28
	%struct.CompressedAssemblyDescriptor {
		i32 36352, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([36352 x i8], [36352 x i8]* @__CompressedAssemblyDescriptor_data_28, i32 0, i32 0); data
	}, 
	; 29
	%struct.CompressedAssemblyDescriptor {
		i32 455168, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([455168 x i8], [455168 x i8]* @__CompressedAssemblyDescriptor_data_29, i32 0, i32 0); data
	}, 
	; 30
	%struct.CompressedAssemblyDescriptor {
		i32 50688, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([50688 x i8], [50688 x i8]* @__CompressedAssemblyDescriptor_data_30, i32 0, i32 0); data
	}, 
	; 31
	%struct.CompressedAssemblyDescriptor {
		i32 4596736, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([4596736 x i8], [4596736 x i8]* @__CompressedAssemblyDescriptor_data_31, i32 0, i32 0); data
	}, 
	; 32
	%struct.CompressedAssemblyDescriptor {
		i32 14728, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14728 x i8], [14728 x i8]* @__CompressedAssemblyDescriptor_data_32, i32 0, i32 0); data
	}, 
	; 33
	%struct.CompressedAssemblyDescriptor {
		i32 424960, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([424960 x i8], [424960 x i8]* @__CompressedAssemblyDescriptor_data_33, i32 0, i32 0); data
	}, 
	; 34
	%struct.CompressedAssemblyDescriptor {
		i32 1370624, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1370624 x i8], [1370624 x i8]* @__CompressedAssemblyDescriptor_data_34, i32 0, i32 0); data
	}, 
	; 35
	%struct.CompressedAssemblyDescriptor {
		i32 73216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([73216 x i8], [73216 x i8]* @__CompressedAssemblyDescriptor_data_35, i32 0, i32 0); data
	}, 
	; 36
	%struct.CompressedAssemblyDescriptor {
		i32 29696, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([29696 x i8], [29696 x i8]* @__CompressedAssemblyDescriptor_data_36, i32 0, i32 0); data
	}, 
	; 37
	%struct.CompressedAssemblyDescriptor {
		i32 78848, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([78848 x i8], [78848 x i8]* @__CompressedAssemblyDescriptor_data_37, i32 0, i32 0); data
	}, 
	; 38
	%struct.CompressedAssemblyDescriptor {
		i32 219648, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([219648 x i8], [219648 x i8]* @__CompressedAssemblyDescriptor_data_38, i32 0, i32 0); data
	}, 
	; 39
	%struct.CompressedAssemblyDescriptor {
		i32 38912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([38912 x i8], [38912 x i8]* @__CompressedAssemblyDescriptor_data_39, i32 0, i32 0); data
	}, 
	; 40
	%struct.CompressedAssemblyDescriptor {
		i32 7680, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([7680 x i8], [7680 x i8]* @__CompressedAssemblyDescriptor_data_40, i32 0, i32 0); data
	}, 
	; 41
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5632 x i8], [5632 x i8]* @__CompressedAssemblyDescriptor_data_41, i32 0, i32 0); data
	}, 
	; 42
	%struct.CompressedAssemblyDescriptor {
		i32 419328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([419328 x i8], [419328 x i8]* @__CompressedAssemblyDescriptor_data_42, i32 0, i32 0); data
	}, 
	; 43
	%struct.CompressedAssemblyDescriptor {
		i32 55808, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([55808 x i8], [55808 x i8]* @__CompressedAssemblyDescriptor_data_43, i32 0, i32 0); data
	}, 
	; 44
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_44, i32 0, i32 0); data
	}, 
	; 45
	%struct.CompressedAssemblyDescriptor {
		i32 11776, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([11776 x i8], [11776 x i8]* @__CompressedAssemblyDescriptor_data_45, i32 0, i32 0); data
	}, 
	; 46
	%struct.CompressedAssemblyDescriptor {
		i32 79360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([79360 x i8], [79360 x i8]* @__CompressedAssemblyDescriptor_data_46, i32 0, i32 0); data
	}, 
	; 47
	%struct.CompressedAssemblyDescriptor {
		i32 1471488, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1471488 x i8], [1471488 x i8]* @__CompressedAssemblyDescriptor_data_47, i32 0, i32 0); data
	}, 
	; 48
	%struct.CompressedAssemblyDescriptor {
		i32 981504, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([981504 x i8], [981504 x i8]* @__CompressedAssemblyDescriptor_data_48, i32 0, i32 0); data
	}, 
	; 49
	%struct.CompressedAssemblyDescriptor {
		i32 53248, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([53248 x i8], [53248 x i8]* @__CompressedAssemblyDescriptor_data_49, i32 0, i32 0); data
	}, 
	; 50
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16896 x i8], [16896 x i8]* @__CompressedAssemblyDescriptor_data_50, i32 0, i32 0); data
	}, 
	; 51
	%struct.CompressedAssemblyDescriptor {
		i32 509952, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([509952 x i8], [509952 x i8]* @__CompressedAssemblyDescriptor_data_51, i32 0, i32 0); data
	}, 
	; 52
	%struct.CompressedAssemblyDescriptor {
		i32 30720, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([30720 x i8], [30720 x i8]* @__CompressedAssemblyDescriptor_data_52, i32 0, i32 0); data
	}, 
	; 53
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17920 x i8], [17920 x i8]* @__CompressedAssemblyDescriptor_data_53, i32 0, i32 0); data
	}, 
	; 54
	%struct.CompressedAssemblyDescriptor {
		i32 79360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([79360 x i8], [79360 x i8]* @__CompressedAssemblyDescriptor_data_54, i32 0, i32 0); data
	}, 
	; 55
	%struct.CompressedAssemblyDescriptor {
		i32 598528, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([598528 x i8], [598528 x i8]* @__CompressedAssemblyDescriptor_data_55, i32 0, i32 0); data
	}, 
	; 56
	%struct.CompressedAssemblyDescriptor {
		i32 25088, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([25088 x i8], [25088 x i8]* @__CompressedAssemblyDescriptor_data_56, i32 0, i32 0); data
	}, 
	; 57
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([9216 x i8], [9216 x i8]* @__CompressedAssemblyDescriptor_data_57, i32 0, i32 0); data
	}, 
	; 58
	%struct.CompressedAssemblyDescriptor {
		i32 44032, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([44032 x i8], [44032 x i8]* @__CompressedAssemblyDescriptor_data_58, i32 0, i32 0); data
	}, 
	; 59
	%struct.CompressedAssemblyDescriptor {
		i32 175104, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([175104 x i8], [175104 x i8]* @__CompressedAssemblyDescriptor_data_59, i32 0, i32 0); data
	}, 
	; 60
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_60, i32 0, i32 0); data
	}, 
	; 61
	%struct.CompressedAssemblyDescriptor {
		i32 15360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15360 x i8], [15360 x i8]* @__CompressedAssemblyDescriptor_data_61, i32 0, i32 0); data
	}, 
	; 62
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16384 x i8], [16384 x i8]* @__CompressedAssemblyDescriptor_data_62, i32 0, i32 0); data
	}, 
	; 63
	%struct.CompressedAssemblyDescriptor {
		i32 17408, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17408 x i8], [17408 x i8]* @__CompressedAssemblyDescriptor_data_63, i32 0, i32 0); data
	}, 
	; 64
	%struct.CompressedAssemblyDescriptor {
		i32 36864, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([36864 x i8], [36864 x i8]* @__CompressedAssemblyDescriptor_data_64, i32 0, i32 0); data
	}, 
	; 65
	%struct.CompressedAssemblyDescriptor {
		i32 424448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([424448 x i8], [424448 x i8]* @__CompressedAssemblyDescriptor_data_65, i32 0, i32 0); data
	}, 
	; 66
	%struct.CompressedAssemblyDescriptor {
		i32 13312, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([13312 x i8], [13312 x i8]* @__CompressedAssemblyDescriptor_data_66, i32 0, i32 0); data
	}, 
	; 67
	%struct.CompressedAssemblyDescriptor {
		i32 40448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([40448 x i8], [40448 x i8]* @__CompressedAssemblyDescriptor_data_67, i32 0, i32 0); data
	}, 
	; 68
	%struct.CompressedAssemblyDescriptor {
		i32 57856, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([57856 x i8], [57856 x i8]* @__CompressedAssemblyDescriptor_data_68, i32 0, i32 0); data
	}, 
	; 69
	%struct.CompressedAssemblyDescriptor {
		i32 37888, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([37888 x i8], [37888 x i8]* @__CompressedAssemblyDescriptor_data_69, i32 0, i32 0); data
	}, 
	; 70
	%struct.CompressedAssemblyDescriptor {
		i32 1209344, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1209344 x i8], [1209344 x i8]* @__CompressedAssemblyDescriptor_data_70, i32 0, i32 0); data
	}, 
	; 71
	%struct.CompressedAssemblyDescriptor {
		i32 961536, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([961536 x i8], [961536 x i8]* @__CompressedAssemblyDescriptor_data_71, i32 0, i32 0); data
	}, 
	; 72
	%struct.CompressedAssemblyDescriptor {
		i32 264104, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([264104 x i8], [264104 x i8]* @__CompressedAssemblyDescriptor_data_72, i32 0, i32 0); data
	}, 
	; 73
	%struct.CompressedAssemblyDescriptor {
		i32 103424, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([103424 x i8], [103424 x i8]* @__CompressedAssemblyDescriptor_data_73, i32 0, i32 0); data
	}, 
	; 74
	%struct.CompressedAssemblyDescriptor {
		i32 258048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([258048 x i8], [258048 x i8]* @__CompressedAssemblyDescriptor_data_74, i32 0, i32 0); data
	}, 
	; 75
	%struct.CompressedAssemblyDescriptor {
		i32 18072, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([18072 x i8], [18072 x i8]* @__CompressedAssemblyDescriptor_data_75, i32 0, i32 0); data
	}, 
	; 76
	%struct.CompressedAssemblyDescriptor {
		i32 154112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([154112 x i8], [154112 x i8]* @__CompressedAssemblyDescriptor_data_76, i32 0, i32 0); data
	}, 
	; 77
	%struct.CompressedAssemblyDescriptor {
		i32 112128, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([112128 x i8], [112128 x i8]* @__CompressedAssemblyDescriptor_data_77, i32 0, i32 0); data
	}, 
	; 78
	%struct.CompressedAssemblyDescriptor {
		i32 122368, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([122368 x i8], [122368 x i8]* @__CompressedAssemblyDescriptor_data_78, i32 0, i32 0); data
	}, 
	; 79
	%struct.CompressedAssemblyDescriptor {
		i32 1539072, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1539072 x i8], [1539072 x i8]* @__CompressedAssemblyDescriptor_data_79, i32 0, i32 0); data
	}, 
	; 80
	%struct.CompressedAssemblyDescriptor {
		i32 912384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([912384 x i8], [912384 x i8]* @__CompressedAssemblyDescriptor_data_80, i32 0, i32 0); data
	}, 
	; 81
	%struct.CompressedAssemblyDescriptor {
		i32 373248, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([373248 x i8], [373248 x i8]* @__CompressedAssemblyDescriptor_data_81, i32 0, i32 0); data
	}, 
	; 82
	%struct.CompressedAssemblyDescriptor {
		i32 89600, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([89600 x i8], [89600 x i8]* @__CompressedAssemblyDescriptor_data_82, i32 0, i32 0); data
	}, 
	; 83
	%struct.CompressedAssemblyDescriptor {
		i32 98816, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([98816 x i8], [98816 x i8]* @__CompressedAssemblyDescriptor_data_83, i32 0, i32 0); data
	}, 
	; 84
	%struct.CompressedAssemblyDescriptor {
		i32 386560, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([386560 x i8], [386560 x i8]* @__CompressedAssemblyDescriptor_data_84, i32 0, i32 0); data
	}, 
	; 85
	%struct.CompressedAssemblyDescriptor {
		i32 125440, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([125440 x i8], [125440 x i8]* @__CompressedAssemblyDescriptor_data_85, i32 0, i32 0); data
	}, 
	; 86
	%struct.CompressedAssemblyDescriptor {
		i32 2208256, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2208256 x i8], [2208256 x i8]* @__CompressedAssemblyDescriptor_data_86, i32 0, i32 0); data
	}
], align 8; end of 'compressed_assembly_descriptors' array


; compressed_assemblies
@compressed_assemblies = local_unnamed_addr global %struct.CompressedAssemblies {
	i32 87, ; count
	%struct.CompressedAssemblyDescriptor* getelementptr inbounds ([87 x %struct.CompressedAssemblyDescriptor], [87 x %struct.CompressedAssemblyDescriptor]* @compressed_assembly_descriptors, i32 0, i32 0); descriptors
}, align 8


!llvm.module.flags = !{!0, !1, !2, !3, !4, !5}
!llvm.ident = !{!6}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"branch-target-enforcement", i32 0}
!3 = !{i32 1, !"sign-return-address", i32 0}
!4 = !{i32 1, !"sign-return-address-all", i32 0}
!5 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
!6 = !{!"Xamarin.Android remotes/origin/d17-3 @ 030cd63c06d95a6b0d0f563fe9b9d537f84cb84b"}
