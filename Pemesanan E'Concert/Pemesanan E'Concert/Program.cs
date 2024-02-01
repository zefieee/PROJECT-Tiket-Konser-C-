using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

class Program
{
    public static List<int> nomorTiket = new();
    static void Main()
    {
        TampilkanMenu();
    }
    static void TampilkanMenu()
    {
        bool success = false;
        string name, contact;
        //Membuat tampilan awal
        Console.WriteLine();
        Console.Write("                                    SELAMAT DATANG DI E'CONCERT                          ");
        Console.WriteLine();
        Console.WriteLine("       ==============================================================================");

        //Meminta user mengisi data 
        Console.Write("\nMasukkan nama       : ");
        name = Console.ReadLine();
        Console.Write("Masukkan nomor hp   : ");
        contact = Console.ReadLine();
        while (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contact))
        {
            Console.WriteLine("Anda harus mengisi semua informasi yang diperlukan!");
            Console.WriteLine();

            Console.Write(" Masukkan Nama : ");
            name = Console.ReadLine();


            Console.Write("Masukkan Nomor hp : ");
            contact = Console.ReadLine();
        }
        Console.Clear();

        //Menampilkan pemilihan tiket
        Console.Write("--------- SILAHKAN PILIH TIKET -----------");
        Console.WriteLine();
        // Membuat objek aplikasi pemesanan tiket
        PemesananTiketKonser aplikasi = new PemesananTiketKonser();

        // Menambahkan tiket konser ke dalam aplikasi
        aplikasi.TambahTiketKonser("Konser A", "Juicy Luicy", "20 Mei 2023", "Gor Satria, Purwokerto", 200);
        aplikasi.TambahTiketKonser("Konser B", " Vierra", "25 Mei 2023", "Caffe Chuseo, Purwokerto", 150);
        aplikasi.TambahTiketKonser("Konser C", " Denny Caknan", "30 Juli 2023", "Gor Arca, Purwokerto", 250);

        // Menampilkan daftar tiket konser
        aplikasi.TampilkanDaftarTiketKonser();

        string pesanLagi = "ya";

        for (int i = 0; pesanLagi.ToLower() == "ya"; i++)
        {
            // Memilih tiket konser
            Console.Write("PILIH TIKET KONSER (masukkan nomor tiket): ");
            nomorTiket.Add(Convert.ToInt32(Console.ReadLine()));
            TiketKonser tiketPilihan = aplikasi.GetTiketKonser(nomorTiket[i]);

            if (tiketPilihan != null)
            {

                // Memasukkan jumlah tiket yang akan dipesan
                Console.Write("\nMasukkan jumlah tiket yang akan dipesan: ");
                int jumlahTiket = int.Parse(Console.ReadLine());

                // Memesan tiket
                aplikasi.PesanTiket(tiketPilihan, jumlahTiket);


                Console.Write("\n=====================================");
                Console.WriteLine();
                Console.Write("             PEMBAYARAN           ");
                Console.WriteLine();
                Console.Write("=====================================");
                // Menampilkan total harga
                Console.Write("\nTotal harga: " + aplikasi.TotalHarga());

                // Melakukan pembayaran
                Console.Write("\nMasukkan jumlah pembayaran: ");
                double jumlahPembayaran = double.Parse(Console.ReadLine());
                

                if (aplikasi.Bayar(jumlahPembayaran))
                {
                    Console.WriteLine(" SELAMAT TRANSAKSI BERHASIL!");
                    Console.WriteLine(" TIKET BERHASIL DIPESAN");
                    success = true;

                }
                else
                {
                    success = false;
                    Console.WriteLine(" MAAF,TRANSAKSI GAGAL. Kekurangan Pembayaran  ");
                    Console.WriteLine(" TIKET TIDAK DAPAT DIPESAN");
                }
            }
            else
            {
                Console.WriteLine("TIKET TIDAK DAPAT DITEMUKAN");
            }

            // Memesan tiket lagi atau keluar dari perulangan
            Console.WriteLine("\nApakah Anda ingin memesan tiket lagi? (ya/tidak)");
            pesanLagi = Console.ReadLine();
            Console.WriteLine("");
        }

        Console.WriteLine("Terima kasih telah menggunakan aplikasi E' Concert!.");
        if (success)
        {
            //Menampilkan detail tiket konser
            Console.Write("\n=====================================");
            Console.WriteLine();
            Console.Write("             DETAIL TIKET             ");
            Console.WriteLine();
            Console.Write("=====================================");
            for (int i = 0; i < nomorTiket.Count; i++)
            {
                aplikasi.TampilkanStrukKonserPilihan(nomorTiket[i]);
            }
        }
    }
}

class PemesananTiketKonser
{
    public List<TiketKonser> daftarTiket;

    public PemesananTiketKonser()
    {
        daftarTiket = new List<TiketKonser>();
    }

    public void TambahTiketKonser( string nama, string artist, string tanggal, string lokasi, int harga)
    {
        TiketKonser tiket = new TiketKonser(nama, artist, tanggal, lokasi, harga);
        daftarTiket.Add(tiket);
    }

    public void TampilkanDaftarTiketKonser()
    {
        Console.WriteLine("Daftar Tiket Konser:");
        for (int i = 0; i < daftarTiket.Count; i++)
        {
            Console.WriteLine("Nomor Tiket: " + (i + 1));
            Console.WriteLine("Nama: " + daftarTiket[i].Nama);
            Console.WriteLine("artist: " + daftarTiket[i].Artist);
            Console.WriteLine("Tanggal: " + daftarTiket[i].Tanggal);
            Console.WriteLine("Lokasi: " + daftarTiket[i].Lokasi);
            Console.WriteLine("Harga: " + daftarTiket[i].Harga);

            Console.WriteLine();
        }
    }

    public void TampilkanStrukKonserPilihan(int i)
    {
        Console.WriteLine();
        Console.WriteLine("Nomor Tiket: " + (i));
        Console.WriteLine("Nama: " + daftarTiket[i - 1].Nama);
        Console.WriteLine("artist: " + daftarTiket[i - 1].Artist);
        Console.WriteLine("Tanggal: " + daftarTiket[i - 1].Tanggal);
        Console.WriteLine("Lokasi: " + daftarTiket[i - 1].Lokasi);
        Console.WriteLine("Harga: " + daftarTiket[i - 1].Harga);
    }

    public TiketKonser GetTiketKonser(int nomorTiket)
    {
        if (nomorTiket >= 1 && nomorTiket <= daftarTiket.Count)
        {
            return daftarTiket[nomorTiket-1];
        }
        else
        {
            return null;
        }
    }

    public void PesanTiket(TiketKonser tiket, int jumlah)
    {
        tiket.JumlahTiketDipesan += jumlah;
    }

    public double TotalHarga()
    {
        double totalHarga = 0;

        foreach (TiketKonser tiket in daftarTiket)
        {
            totalHarga += tiket.Harga * tiket.JumlahTiketDipesan;
        }

        return totalHarga;
    }




public bool Bayar(double jumlahPembayaran)
    {
        double totalHarga = TotalHarga();
        if (jumlahPembayaran >= totalHarga)
        {
            double kembalian = jumlahPembayaran - totalHarga;
            Console.WriteLine("Kembalian: " + kembalian);
            return true;
        }
        else
        {
            return false;
        }
    }
}

class TiketKonser
{
    public string Nama { get; set; }
    public string Artist { get; set; }
    public string Tanggal { get; set; }
    public string Lokasi { get; set; }
    public int Harga { get; set; }
    public int JumlahTiketDipesan { get; set; }

    public TiketKonser(string nama, string artist, string tanggal, string lokasi, int harga)
    {
        Nama = nama;
        Artist = artist;
        Tanggal = tanggal;
        Lokasi = lokasi;
        Harga = harga;
        JumlahTiketDipesan = 0;
    }
}