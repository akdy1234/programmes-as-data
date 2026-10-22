//Copied broken version over and added line 67

class QueueFixed {
  public static void main(String[] args) {
    for (int threads=1; threads<20; threads++)
      runThreads(threads, new SentinelLockQueue());
  }

  private static void runThreads(final int threads, final Queue queue) {
    final int iterations = 200000000; // Increase this constant if program does not run out of memory.
    final Timer timer = new Timer();
    Thread[] ts = new Thread[threads];
    queue.put(-6);
    queue.put(-5);
    queue.put(-4);
    queue.put(-3);
    queue.put(-2);
    queue.put(-1);
    for (int j=0; j<threads; j++)
      ts[j] = new Thread() {
          public void run() {
            for (int i=0; i<iterations; i++) {
              queue.put(i);
              queue.get();
            }
          }
        };
    for (int j=0; j<threads; j++)
      ts[j].start();
    try {
      for (int j=0; j<threads; j++)
	  ts[j].join();  // Wait for thread j to terminate.
    } catch (Exception exn) {
      System.out.println(exn);
    }
    System.out.printf("%-20s\t%4d\t%7.2f\t%s%n",
                      queue.getClass().getName(), threads, timer.Check(), queue.get());
  }
}

interface Queue {
  boolean put(int item);
  int get();
}

// --------------------------------------------------
// Locking queue, with sentinel (dummy) node

class SentinelLockQueue implements Queue {
  // With sentinel (dummy) node.
  // Invariants:
  //  * The node referred by tail is reachable from head.
  //  * If non-empty then head != tail,
  //     and tail points to last item, and head.next to first item.
  //  * If empty then head == tail.

  private static class Node {
    final int item;
    volatile Node next;

    public Node(int item, Node next) {
      this.item = item;
      this.next = next;
    }
  }

  private Node head = new Node(-444, null), tail = head;

  public synchronized boolean put(int item) {
    Node node = new Node(item, null);
    tail.next = node;
    tail = node;
    return true;
  }

  public synchronized int get() {
    if (head.next == null)
      return -999;
    Node first = head;
    head = first.next;
    return head.item;
  }
}

// Crude timing utility ----------------------------------------

class Timer {
  private long start, spent = 0;
  public Timer() { Play(); }
  public double Check() { return (System.nanoTime()-start+spent)/1E9; }
  public void Pause() { spent += System.nanoTime()-start; }
  public void Play() { start = System.nanoTime(); }
}
