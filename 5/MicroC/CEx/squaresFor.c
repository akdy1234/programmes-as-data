// micro-C exercise 7.3 -- squares.c rewritten to use for-loops

void squares(int n, int arr[]) {
  int i;
  for (i = 0; i < n; i = i + 1) {
    arr[i] = i * i;
  }
}

void arrsum(int n, int arr[], int *sump) {
  int i;
  int sum;
  sum = 0;
  for (i = 0; i < n; i = i + 1) {
    sum = sum + arr[i];
  }
  *sump = sum;
}

void main(int n) {
  int arr[20];
  int sum;
  squares(n, arr);
  arrsum(n, arr, &sum);
  print sum;
  println;
}
