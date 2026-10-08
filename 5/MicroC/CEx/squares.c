// micro-C exercise 7.2 (ii) -- fill an array with squares 0,1,4,9,...
// then reuse arrsum from exercise 7.2 (i) to add them up

void squares(int n, int arr[]) {
  int i;
  i = 0;
  while (i < n) {
    arr[i] = i * i;
    i = i + 1;
  }
}

void arrsum(int n, int arr[], int *sump) {
  int i;
  int sum;
  i = 0;
  sum = 0;
  while (i < n) {
    sum = sum + arr[i];
    i = i + 1;
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
