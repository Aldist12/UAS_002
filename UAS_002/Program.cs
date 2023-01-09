using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAS_002
{
    class Node
    {
        /*Node class represent the node of doubly linked list
          * It consists of the information part and links to
          * its succeding and preceeding
          * in terms of next and previous */
        public int noNim;
        public string name;
        public string kelas;
        //point to the succeding node
        public Node next;
        //point to the preceeding node
        public Node prev;
    }

    class DoubleLinkedList
    {
        Node START;

        //constructor

        public void addNode()
        {
            int nim;
            string nm;
            string kls;
            Console.WriteLine("\nEnter the roll number of the student: ");
            nim = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nEnter the name of the student: ");
            nm = Console.ReadLine();
            Console.Write("\nEnter the class of the student: ");
            kls = Console.ReadLine();
            Node newNode = new Node();
            newNode.noNim = nim;
            newNode.name = nm;
            newNode.kelas = kls;

            //check if the list empty
            if (START == null || nim <= START.noNim)
            {
                if ((START != null) && (nim == START.noNim))
                {
                    Console.WriteLine("\nDuplicate number not allowed");
                    return;
                }
                newNode.next = START;
                if (START != null)
                    START.prev = newNode;
                newNode.next = null;
                START = newNode;
                return;
            }
            /*if the node is to be inserted at beetwen two Node*/
            Node previous, current;
            for (current = previous = START;
                current != null && nim >= current.noNim;
                previous = current, current = current.next)
            {
                if (nim == current.noNim)
                {
                    Console.WriteLine("\nDuplicate roll numbers not allowed");
                    return;
                }
            }
            /*On the execution of the above for loop, prev and 
            * current will point to those nodes
            * between which the new node is to be inserted */
            newNode.next = current;
            newNode.prev = previous;

            //if the node is to be inserted at the end of the list
            if (current == null)
            {
                newNode.next = null;
                previous.next = newNode;
                return;
            }
            current.prev = newNode;
            previous.next = newNode;
        }

        public bool Search(int rollNo, ref Node previous, ref Node current)
        {
            previous = current = START;
            while (current != null &&
                rollNo != current.noNim)
            {
                previous = current;
                current = current.next;
            }
            return (current != null);
        }
        public bool dellNode(int rollNo)
        {
            Node previous, current;
            previous = current = null;
            if (Search(rollNo, ref previous, ref current) == false)
                return false;
            // the begining of data
            if (current.next == null)
            {
                previous.next = null;
                return true;
            }
            //Node between two nodes in the list
            if (current == START)
            {
                START = START.next;
                if (START != null)
                    START.prev = null;
                return true;
            }

            /*if the to be deleted is in between the list then the following lines of is executed. */
            previous.next = current.next;
            current.next.prev = previous;
            return true;
        }

        public bool listEmpty()
        {
            if (START == null)
                return true;
            else
                return false;
        }

        public void ascending()
        {
            if (listEmpty())
                Console.WriteLine("\nList is empty");
            else
            {
                Console.WriteLine("\nRecord in the ascending order of" + "Roll number are:\n");
                Node currentNode;
                for (currentNode = START; currentNode != null; currentNode = currentNode.next)
                    Console.Write(currentNode.noNim + currentNode.name + "\n");
            }
        }

        public void descending()
        {
            if (listEmpty())
                Console.WriteLine("\nList is empty");
            else
            {
                Console.WriteLine("Record in the Descending order of" + "Roll number are:\n");
                Node currentNode;
                //membawa currentNode ke node paling belakang
                currentNode = START;
                while (currentNode.next != null)
                {
                    currentNode = currentNode.next;
                }

                //membaca data dari last node ke first node
                while (currentNode != null)
                {
                    Console.Write(currentNode.noNim + " " + currentNode.name + "\n");
                    currentNode = currentNode.prev;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DoubleLinkedList obj = new DoubleLinkedList();
            while (true)
            {
                try
                {
                    Console.WriteLine("\nPilihan Menu");
                    Console.WriteLine("1. Menambahkan NIM, NAMA & KELAS");
                    Console.WriteLine("2. Menghapus data yang tersimpan");
                    Console.WriteLine("3. Menampilkan NIM secara Urut");
                    Console.WriteLine("4. Menampilkan NIM dari yang terbesar");
                    Console.WriteLine("5. Mencari Nama dengan NIM ");
                    Console.WriteLine("6. Keluar\n");
                    Console.WriteLine("Tekan pilihan dari (1-6): ");
                    char ch = Convert.ToChar(Console.ReadLine());
                    switch (ch)
                    {
                        case '1':
                            {
                                obj.addNode();
                            }
                            break;
                        case '2':
                            {
                                if (obj.listEmpty())
                                {
                                    Console.WriteLine("\nList is empty");
                                    break;
                                }
                                Console.Write("\nEnter the roll number of the student" +
                                    "whose record is to be deleted: ");
                                int rollNo = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine();
                                if (obj.dellNode(rollNo) == false)
                                    Console.WriteLine("Record not found");
                                else
                                    Console.WriteLine("Record with roll number" + rollNo + "deleted \n");

                            }
                            break;
                        case '3':
                            {
                                obj.ascending();
                            }
                            break;
                        case '4':
                            {
                                obj.descending();
                            }
                            break;
                        case '5':
                            {
                                if (obj.listEmpty() == true)
                                {
                                    Console.WriteLine("\nList is empty");
                                    break;
                                }
                                Node prev, curr;
                                prev = curr = null;
                                Console.Write("\nEnter the roll number of the student whose record you want to search: ");
                                int num = Convert.ToInt32(Console.ReadLine());
                                if (obj.Search(num, ref prev, ref curr) == false)
                                    Console.WriteLine("\nRecord not found");
                                else
                                {
                                    Console.WriteLine("\nRecord found");
                                    Console.WriteLine("\nRoll number: " + curr.noNim);
                                    Console.WriteLine("\nName: " + curr.name);
                                }
                                break;
                            }
                        case '6':
                            return;
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Check for the values entered.");
                }
            }
        }
    }
}



//2.double_linked_list
//3.penambahan dan penghapusan elemen pada posisi akhir stack
//4.add & delete
//5.a.4 kedalaman b.post order traversal