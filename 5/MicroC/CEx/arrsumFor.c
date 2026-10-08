// micro-C exercise 7.3 -- arrsum.c rewritten to use a for-loop

void arrsum(int n, int arr[], int *sump) {
  int i;
  int sum;
  sum = 0;
  for (i = 0; i < n; i = i + 1) {
    sum = sum + arr[i];
  }
  *sump = sum;
}

void main() {
  int arr[4];
  int sum;
  arr[0] = 7;
  arr[1] = 13;
  arr[2] = 9;
  arr[3] = 8;
  arrsum(4, arr, &sum);
  print sum;
  println;
}
