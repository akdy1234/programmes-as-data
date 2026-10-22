.method public hidebysig static void  SelectionSort(int32[] arr) cil managed
{
  // Code size       57 (0x39)
  .maxstack  4
  .locals init (int32 V_0,  //i
           int32 V_1,       //least
           int32 V_2,       //j
           int32 V_3)       //tmp
  IL_0000:  ldc.i4.0        //push 0
  IL_0001:  stloc.0         //i = 0
  IL_0002:  br.s       IL_0032  //jump down to the outer loop test (i < arr.Length)

  IL_0004:  ldloc.0         //push i
  IL_0005:  stloc.1         //least = i
  IL_0006:  ldloc.0         //push i
  IL_0007:  ldc.i4.1        //push 1
  IL_0008:  add             //i + 1
  IL_0009:  stloc.2         //j = i + 1
  IL_000a:  br.s       IL_001a  //jump down to the inner loop test (j < arr.Length)

  IL_000c:  ldarg.0         //push arr
  IL_000d:  ldloc.2         //push j
  IL_000e:  ldelem.i4       //push arr[j]
  IL_000f:  ldarg.0         //push arr
  IL_0010:  ldloc.1         //push least
  IL_0011:  ldelem.i4       //push arr[least]
  IL_0012:  bge.s      IL_0016  //if arr[j] >= arr[least] skip the update (if-body is empty)

  IL_0014:  ldloc.2         //push j
  IL_0015:  stloc.1         //least = j
  IL_0016:  ldloc.2         //push j
  IL_0017:  ldc.i4.1        //push 1
  IL_0018:  add             //j + 1
  IL_0019:  stloc.2         //j = j + 1
  IL_001a:  ldloc.2         //push j
  IL_001b:  ldarg.0         //push arr
  IL_001c:  ldlen           //push arr.Length (as native uint)
  IL_001d:  conv.i4         //convert it to int32
  IL_001e:  blt.s      IL_000c  //if j < arr.Length go back to the inner loop body

  IL_0020:  ldarg.0         //push arr
  IL_0021:  ldloc.0         //push i
  IL_0022:  ldelem.i4       //push arr[i]
  IL_0023:  stloc.3         //tmp = arr[i]
  IL_0024:  ldarg.0         //push arr
  IL_0025:  ldloc.0         //push i
  IL_0026:  ldarg.0         //push arr
  IL_0027:  ldloc.1         //push least
  IL_0028:  ldelem.i4       //push arr[least]
  IL_0029:  stelem.i4       //arr[i] = arr[least]
  IL_002a:  ldarg.0         //push arr
  IL_002b:  ldloc.1         //push least
  IL_002c:  ldloc.3         //push tmp
  IL_002d:  stelem.i4       //arr[least] = tmp
  IL_002e:  ldloc.0         //push i
  IL_002f:  ldc.i4.1        //push 1
  IL_0030:  add             //i + 1
  IL_0031:  stloc.0         //i = i + 1
  IL_0032:  ldloc.0         //push i
  IL_0033:  ldarg.0         //push arr
  IL_0034:  ldlen           //push arr.Length (as native uint)
  IL_0035:  conv.i4         //convert it to int32
  IL_0036:  blt.s      IL_0004  //if i < arr.Length go back to the outer loop body

  IL_0038:  ret             //done, SelectionSort is void so nothing is pushed back
} // end of method Selsort::SelectionSort
