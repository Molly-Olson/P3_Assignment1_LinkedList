using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace P3_Assignment1_LinkedList
{
    public class Node
    {
        public int Value;
        public Node Next;
        public Node(int value)
        {
            Value = value;
        }
    }
    public class MyList
    {
        // 'sticky note' on the first box which is empty currently
        private Node? head;
        // 'sticky' on Last box
        private Node? tail;
        // how many boxes are in the chain?
        private int count;
        // add a new number to the end of the chain
        public void Add(int value)
        {
            // make new box holding the number
            Node newNode = new Node(value);
            //if chain is empty, new box is both head and tail
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            // otherwise hook it onto the last box then move the last sticky note
            else if (tail != null)
            {
                tail.Next = newNode;
                tail = newNode;
            }
                // one more box in the chain
                count++;
        }
        // print walk the chain from fist to last
        public void Print()
        {
            // put your finger on the first box
            Node? current = head;
            // keep going while finger is on a real box
            while (current != null)
            {
                Console.Write(current.Value + " ");
                current = current.Next;  // move your finger to the next box
            }
            Console.WriteLine();
        }
        public void InsertAt(int position, int value)
        {
            // make a new box holding the number
            Node newNode = new Node(value);
            // came back to handle special case insert at front of list
            if (position == 0)
            {
                newNode.Next = head; // new box grabs the old first box
                head = newNode; // "first" sticky attached to new box
                if (tail == null)  //if the list was empty
                {
                    tail = newNode;  // the new box would also be the last box
                }
                count++;
                return;
            }
            // walk to the box before desired index position keep stepping until reached
            Node? previous = head;
            for (int i = 0; i < position - 1 && previous != null; i++)
            {
                previous = previous.Next;
            }
            // safe check if we walk off end that position doesn't exist
            if (previous == null)
            {
                Console.WriteLine("That position is just a figment of your imagination my friend.");
                return;
            }
            // new box grabs the box after previous
            newNode.Next = previous.Next;
            // previous lets go and grabs new box
            previous.Next = newNode;
            // one more box in chain
            count++;
        }
        public void DeleteAt(int position) // what about deleting 15 when you don't know the position number 
        {
            // back to handle special case of removing position zero
            if (position == 0)
            {
                if (head == null)
                {
                    Console.WriteLine("This list is empty hommie.");
                    return;
                }
                head = head.Next; // "first" sticky note moves to second box
                if (head == null) // if that emptied the list then...
                {
                    tail = null; // the last sticky is removed also
                }
                count--;
                return;
            }
            // following InsertAt to remove elements from linked list
            Node? previous = head;
            for (int i =0; i < position - 1 && previous != null; i++)
            {
                previous = previous.Next;
            }
            // saftey meeting make sure both boxes exist
            if (previous == null || previous.Next == null)
            {
                Console.WriteLine("That is just a fart in the wind my friend, nothing here!");
                return;
            }
            // second finger on the box to remove
            Node removed = previous.Next;
            // previous skips over it and grabs box after instead
            previous.Next = removed.Next;
            // if we remove the last box move the last sticky also (tail pointer)
            if (removed == tail)
            {
                tail = previous;
            }
            // count drops by one as ther is one less box in the chain
            count--;
        }
        public void Delete(int value)
        {
            // check for empty list
            if (head == null)
            {
                Console.WriteLine("List is empty Broseph!");
                return; // return means stop here leave this method and go back where you came from
            }
            // is the number in the first box?
            if (head.Value == value)
            {
                head = head.Next; // the first sticky gets moved to the second box
                if (head == null)  // if that emptied the list then
                {
                    tail = null; // last sticky is removed also
                }
                count--;
                return;
            }
            // place two fingers, one on the box before and one on the box we are looking at
            Node previous = head;
            Node? current = head.Next;
            while (current != null)
            {
                // found value then previous skips over it
                if (current.Value == value)
                {
                    previous.Next = current.Next;
                    // if last box move 'last' sticky back
                    if (current == tail)
                    {
                        tail = previous;
                    }
                    count--;
                    return; 
                }
                // not this one so move both fingers forward
                previous = current;
                current = current.Next;
            }
            Console.WriteLine(value + " is not in this list my good sir.");
        }

    }
        public class Program
        {

        static void Main()
        {
            Console.WriteLine("Welcome to my Linked List assignment Bruh!");

            MyList numbers = new MyList();

            numbers.Add(10);
            numbers.Add(15);
            numbers.Add(20);
            numbers.Add(25);

            Console.WriteLine("Elements in the LinkedList;");
            numbers.Print();
            numbers.InsertAt(1, 12);
            numbers.Print();
            numbers.DeleteAt(2);
            numbers.Print();
            numbers.Delete(25);
            numbers.Print();
            numbers.Delete(99);

            // adding a menu so user can add and delete numbers
            Console.Write("Your current list..."); // Write vs WriteLine = write will print and stay line goes to next line
            numbers.Print();
            // on/off switch, menu keeps going while this is true
            bool running = true;
            while (running)
            {
                // show the menu
                Console.WriteLine();
                Console.WriteLine("1 = Add a number to the end");
                Console.WriteLine("2 = Insert at a position");
                Console.WriteLine("3 = Remove by position");
                Console.WriteLine("4 = Remove by number value");
                Console.WriteLine("5 = Quit");
                Console.WriteLine("Pick your poison my friend!");

                // read user input
                string? choice = Console.ReadLine();
                // make the selection 1-5 do what it is supposed to
                if (choice == "1")
                {
                    Console.Write("Number to add: ");
                    int value = int.Parse(Console.ReadLine());
                    numbers.Add(value);
                }
                else if (choice == "2")
                {
                    Console.Write("Number to insert: ");
                    int value = int.Parse(Console.ReadLine());
                    Console.Write("Where would you like this positioned in the list (starts at zero): ");
                    int position = int.Parse(Console.ReadLine());
                    numbers.InsertAt(position, value);
                }
                else if (choice == "3")
                {
                    Console.Write("What position would you like removed? (index starts at position 0)");
                    int position = int.Parse(Console.ReadLine());
                    numbers.DeleteAt(position);
                }
                else if (choice == "4")
                {
                    Console.Write("Remove which number from list: ");
                    int value = int.Parse(Console.ReadLine());
                    numbers.Delete(value);
                }
                else if (choice == "5")
                {
                    running = false; // turning switch to off so loop ends
                }
                else
                {
                    Console.WriteLine("Choose 1-5 for funsies...");
                }
                if (running)
                {
                    Console.Write("Current List: ");
                    numbers.Print();
                }
            }
            Console.WriteLine("Later Alligator!");
        }
    }
}
