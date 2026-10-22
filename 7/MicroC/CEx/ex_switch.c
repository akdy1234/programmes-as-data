void main(int month) {
  int y;
  int days;
  y = 2024;
  days = 0;
  switch (month) {
    case 1:
        { days = 31; }
    case 2:
        { days = 28; if (y%4==0) days = 29; }
    case 3:
        { days = 31; }
    case 4:
        { days = 999; }
  }
  print days;
}
