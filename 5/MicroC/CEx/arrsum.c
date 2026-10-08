// micro-C exercise 7.2 (i) -- sum the first n elements of an array,
// returning the result through a pointer argument

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
