void main() {
  int i;
  int arr[5];
  i = 0;
  while (i < 5) {
    arr[i] = i * 10;
    i = i + 1;
  }
  i = 0;
  print ++i;
  print --i;
  println;
  ++arr[++i];
  print i;
  print arr[0];
  print arr[1];
  print arr[2];
  println;
  --arr[++i];
  print i;
  print arr[1];
  print arr[2];
  println;
}
