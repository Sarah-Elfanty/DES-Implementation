using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DES_Implementation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int[] PC1 =
        {
            57, 49, 41, 33, 25, 17, 9,
            1, 58, 50, 42, 34, 26, 18,
            10, 2, 59, 51, 43, 35, 27,
            19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7, 62, 54, 46, 38, 30, 22,
            14, 6, 61, 53, 45, 37, 29,
            21, 13, 5, 28, 20, 12, 4
        };

        public static int[] PC2 =
        {
            14, 17, 11, 24, 1, 5,
            3, 28, 15, 6, 21, 10,
            23, 19, 12, 4, 26, 8,
            16, 7, 27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };

        public static int[] IP =
        {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };

        public static int[] E =
        {
            32, 1, 2, 3, 4, 5,
            4, 5, 6, 7, 8, 9,
            8, 9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };

        static int[, ,] sboxes = 
        {
            //sbox1
            {
                {14,  4, 13,  1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9,  0,  7},
                { 0, 15,  7,  4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8},
                { 4,  1, 14,  8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0},
                {15, 12,  8,  2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0,  6, 13},
            },
            //sbox2
            {
                {15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10},
                { 3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5},
                { 0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15},
                {13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9},
            },
            //sbox3
            {
                {10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8},
                {13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1},
                {13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7},
                { 1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12},
            },
            //sbox4
            {
                { 7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15},
                {13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9},
                {10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4},
                { 3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14},
            },
            //sbox5
            {
                { 2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9},
                {14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6},
                { 4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14},
                {11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3},
            },
            //sbox6
            {
                {12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11},
                {10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8},
                { 9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6},
                { 4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13},
            },
            //sbox7
            {
                { 4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1},
                {13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6},
                { 1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2},
                { 6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12},
            },
            //sbox8
            {
                {13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7},
                { 1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2},
                { 7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8},
                { 2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11},
            }
        };

        static int[] P =
        {
            16, 7, 20, 21,
            29, 12, 28, 17,
            1, 15, 23, 26,
            5, 18, 31, 10,
            2, 8, 24, 14,
            32, 27, 3, 9,
            19, 13, 30, 6,
            22, 11, 4, 25
        };

        public static int[] Initial_permutation_inv =
        {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25
        };

        public static int[] LeftShifts = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        static int[] getbinary(string str)
        {
            string[] Str = new string[str.Length];
            int x = 0;
            foreach (char element in str)
            {
                Str[x] = element.ToString();
                x++;
            }
            int[] numbers = new int[IP.Length];
            int y = 0;
            for (int i = 0; i < Str.Length; i++)
            {
                string character = Str[i].ToString();
                string output = Convert.ToString(Convert.ToInt32(character, 16), 2).PadLeft(4, '0');
                foreach (char t in output)
                {
                    string T = t.ToString();
                    numbers[y] = Convert.ToInt32(T);
                    y++;
                }
            }
            if (numbers.Length < IP.Length)
            {
                for (int m = y; m < IP.Length; m++)
                    numbers[m] = 0;
            }
            return numbers;
        }

        static int[] getpc1(int[] binary)
        {
            int[] pc1_output = new int[binary.Length - 8];
            for (int i = 0; i < pc1_output.Length; i++)
                pc1_output[i] = binary[PC1[i] - 1];
            return pc1_output;
        }

        static int[] leftshift(int[] pc1)
        {

            int[] output = new int[56];
            int[] c = new int[28];
            int[] d = new int[28];
            int i = 1;
            int j = 0;
            while (i <= 28)
            {
                if (i == 28)
                    c[j] = pc1[0];
                else
                    c[j] = pc1[i];
                j++;
                i++;
            }
            c.CopyTo(output, 0);
            int l = 0;
            while (i <= 56)
            {
                if (i == 56)
                    d[l] = pc1[28];
                else
                    d[l] = pc1[i];
                i++;
                l++;
            }
            d.CopyTo(output, 28);
            return output;
        }

        static int[] getpc2(int[] shifted)
        {
            int[] pc2_output = new int[PC2.Length];
            for (int i = 0; i < pc2_output.Length; i++)
                pc2_output[i] = shifted[PC2[i] - 1];
            return pc2_output;
        }

        static int[] getinitialpermutation(int[] binary)
        {
            int[] IP_output = new int[IP.Length];
            for (int i = 0; i < IP_output.Length; i++)
                IP_output[i] = binary[IP[i] - 1];
            return IP_output;
        }

        static int[] getexpansion(int[] R)
        {
            int[] expansion_output = new int[E.Length];
            for (int i = 0; i < E.Length; i++)
                expansion_output[i] = R[E[i] - 1];
            return expansion_output;
        }

        static int[] xor_function(int[] operator1, int[] operator2)
        {
            int[] xor_output = new int[operator1.Length];

            for (int i = 0; i < operator1.Length; i++)
            {
                if (operator1[i] == operator2[i])
                    xor_output[i] = 0;
                else
                    xor_output[i] = 1;
            }

            return xor_output;
        }

        static int[] getsbox_output(int[] xor_output)
        {
            int[,] sbox_indeces = new int[8, 2];
            int[,] output = new int[8, 6];
            int[] sbox_output = new int[32];
            int l = 0;
            for (int i = 0; i < 8; i++)
            {
                int k = 0;
                while (k < 6)
                {
                    output[i, k] = xor_output[l];
                    k++;
                    l++;
                }

            }
            for (int j = 0; j < 8; j++)
            {
                StringBuilder row = new StringBuilder();
                StringBuilder col = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0 || i == 5)
                        row.Append(output[j, i].ToString());

                    else
                        col.Append(output[j, i].ToString());
                }
                string rows = row.ToString();
                string cols = col.ToString();
                int index1 = Convert.ToInt32(rows, 2);
                int index2 = Convert.ToInt32(cols, 2);
                sbox_indeces[j, 0] = index1;
                sbox_indeces[j, 1] = index2;
            }
            int m = 0;
            for (int i = 0; i < 8; i++)
            {
                string sbox_out = Convert.ToString(Convert.ToInt32(sboxes[i, sbox_indeces[i, 0], sbox_indeces[i, 1]].ToString(), 10), 2).PadLeft(4, '0');
                foreach (char t in sbox_out)
                {
                    string x = t.ToString();
                    sbox_output[m] = Convert.ToInt32(x);
                    m++;
                }
            }

            return sbox_output;
        }

        static int[] getpermutation(int[] sbox_output)
        {
            int[] permutation_ouptut = new int[P.Length];
            for (int i = 0; i < P.Length; i++)
                permutation_ouptut[i] = sbox_output[P[i] - 1];
            return permutation_ouptut;
        }

        static int[] getinverse_permutation(int[] swapped)
        {
            int[] cipher = new int[swapped.Length];
            for (int i = 0; i < swapped.Length; i++)
                cipher[i] = swapped[Initial_permutation_inv[i] - 1];
            return cipher;
        }

        static string getcipher(int[] inverse_permutation_output)
        {
            string[] hex = new string[16];
            StringBuilder strbuilder = new StringBuilder();
            //int s = 0;
            for (int i = 0; i < inverse_permutation_output.Length; i = i + 4)
            {
                StringBuilder binary = new StringBuilder();
                for (int j = 0; j < 4; j++)
                {
                    binary.Append(inverse_permutation_output[i + j]);
                }
                string hexa = Convert.ToUInt16(binary.ToString(), 2).ToString("X");
                strbuilder.Append(hexa);
            }


            return strbuilder.ToString();
        }

        static int[,] generate_keys(string key)
        {
            int[] pc1 = getpc1(getbinary(key));
            int[,] keys = new int[16, 48];
            int[] shifted = leftshift(pc1);
            int[] pc2_output = getpc2(shifted);
            for (int k = 0; k < 48; k++)
                keys[0, k] = pc2_output[k];

            for (int i = 1; i <= 15; i++)
            {
                for (int b = 0; b < LeftShifts[i]; b++)
                    shifted = leftshift(shifted);
                pc2_output = getpc2(shifted);
                for (int j = 0; j < 48; j++)
                    keys[i, j] = pc2_output[j];
            }
            return keys;
        }

        static string encrypt(string plaintext, string key)
        {
            int[] plaintext_binary = getbinary(plaintext);
            int[] IP_output = getinitialpermutation(plaintext_binary);

            int[] msb = new int[32];
            int[] lsb = new int[32];
            for (int k = 0; k < msb.Length; k++)
            {
                msb[k] = IP_output[k];
                lsb[k] = IP_output[k + 32];
            }

            int[] new_lsb = new int[32];
            int[,] keys = generate_keys(key);

            for (int l = 0; l < 16; l++)
            {

                int[] expansion_output = getexpansion(lsb);
                int[] pc2 = new int[48];
                for (int g = 0; g < 48; g++)
                {
                    pc2[g] = keys[l, g];
                }

                int[] xor_output = xor_function(expansion_output, pc2);
                int[] sbox_output = getsbox_output(xor_output);
                int[] permutation_output = getpermutation(sbox_output);

                new_lsb = xor_function(msb, permutation_output);
                lsb.CopyTo(msb, 0);
                new_lsb.CopyTo(lsb, 0);
            }
            int[] swapped = new int[64];
            for (int i = 0; i < 32; i++)
            {
                swapped[i] = lsb[i];
                swapped[i + 32] = msb[i];
            }
            int[] cipher_binary = getinverse_permutation(swapped);
            string ciphertext = getcipher(cipher_binary);
            return ciphertext;
        }

        static string decrypt(string plaintext, string key)
        {
            int[] plaintext_binary = getbinary(plaintext);
            int[] IP_output = getinitialpermutation(plaintext_binary);

            int[] msb = new int[32];
            int[] lsb = new int[32];
            for (int k = 0; k < msb.Length; k++)
            {
                msb[k] = IP_output[k];
                lsb[k] = IP_output[k + 32];
            }

            int[] new_lsb = new int[32];

            int[,] encryption_keys = generate_keys(key);

            for (int l = 15; l >= 0; l--)
            {


                int[] expansion_output = getexpansion(lsb);
                int[] pc2 = new int[48];
                for (int k = 0; k < 48; k++)
                {
                    pc2[k] = encryption_keys[l, k];
                }

                int[] xor_output = xor_function(expansion_output, pc2);
                int[] sbox_output = getsbox_output(xor_output);
                int[] permutation_output = getpermutation(sbox_output);

                new_lsb = xor_function(msb, permutation_output);
                lsb.CopyTo(msb, 0);
                new_lsb.CopyTo(lsb, 0);
            }
            int[] swapped = new int[64];
            for (int i = 0; i < 32; i++)
            {
                swapped[i] = lsb[i];
                swapped[i + 32] = msb[i];
            }
            int[] cipher_binary = getinverse_permutation(swapped);
            string ciphertext = getcipher(cipher_binary);
            return ciphertext;
        }

        static List<string> getsubstrings(string plaintext)
        {
            int Strings = plaintext.Length / 16;
            List<string> substrings = new List<string>();
            int index = 0;
            int count = 0;
            //StringBuilder output = new StringBuilder();           
            while (Strings > 0)
            {
                StringBuilder strbuilder = new StringBuilder();
                for (int j = 0; j < 16; j++)
                {
                    strbuilder.Append(plaintext[index]);
                    index++;
                }
                substrings.Add(strbuilder.ToString());
                count += 16;
                Strings--;
            }
            if (count < plaintext.Length)
            {
                StringBuilder strbuilder1 = new StringBuilder();
                for (int k = index; k < plaintext.Length; k++)
                    strbuilder1.Append(plaintext[k]);
                substrings.Add(strbuilder1.ToString());
            }
            return substrings;
        }


        private void Encryption_Key_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please make sure you filled in the required data");
            }
            else
            {
                //textBox3.Clear();
                string key = textBox5.Text;
                if (key.Length != 16)
                {
                    MessageBox.Show("key must be 16 integers");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
                else
                {
                    string plaintext = textBox1.Text;
                    textBox1.Clear();
                    textBox5.Clear();
                    textBox4.Clear();
                    string ciphertext = "";
                    List<string> substrings = new List<string>();
                    if (plaintext.Length > 16)
                    {
                        substrings = getsubstrings(plaintext);
                        StringBuilder strbuilder = new StringBuilder();
                        foreach (string substring in substrings)
                        {
                            strbuilder.Append(encrypt(substring, key));
                        }
                        ciphertext = strbuilder.ToString();
                    }

                    else
                        ciphertext = encrypt(plaintext, key);
                    textBox2.Text = ciphertext;
                }
            }
        }

        private void Decryption_Key_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please make sure you filled in the required data");
            }
            else
            {
                //textBox3.Clear();
                string key = textBox5.Text;

                if (key.Length != 16)
                {
                    MessageBox.Show("key must be 16 integers");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
                else
                {
                    string plaintext = textBox3.Text;
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox5.Clear();
                    string ciphertext = "";
                    List<string> substrings = new List<string>();
                    if (plaintext.Length > 16)
                    {
                        substrings = getsubstrings(plaintext);
                        StringBuilder strbuilder = new StringBuilder();
                        foreach (string substring in substrings)
                        {
                            strbuilder.Append(decrypt(substring, key));
                        }
                        ciphertext = strbuilder.ToString();
                    }

                    else
                        ciphertext = decrypt(plaintext, key);
                    textBox4.Text = ciphertext;
                }
            }
        }
    }
}
