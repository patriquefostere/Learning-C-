using System;
using System.Windows.Forms;
using System.Media;
using System.IO;


public class frmApp : Form
{   
    public frmApp()
    {
        InitializeComponent();
    }

    private Button b;

    protected void bClick(object sender, EventArgs e)
    {
        this.b.Text = "button clicked";
        SoundPlayer player = new SoundPlayer();
 
        player.SoundLocation = @"C:\Users\Surface\Desktop\coding\C#\messing with audio\honk.wav";
        player.Play();
    }
    
    private void doButton()
    {
        this.Text = "audio app";
        this.ControlBox = true;
        this.AutoScroll = true;
        this.KeyPreview = true;
        
        b = new Button();
        b.Text = "play";
        b.AutoSize = true;
        this.Controls.Add(b);
        
        //click event handler:
        b.Click += new System.EventHandler(this.bClick);
    }
    
    private void InitializeComponent()
    {
        
        doButton();

        string file = @"C:\Users\Surface\Desktop\coding\C#\messing with audio\honk.wav";

        byte[] array = File.ReadAllBytes(file);
        
        /*Console.WriteLine("First byte: {0}", array[0]);
        Console.WriteLine("Last byte: {0}",
            array[array.Length - 1]);
        Console.WriteLine(array.Length);
        foreach (var item in array)
        {
            Console.WriteLine(item.ToString());
        }*/
    
    }
    
    public static void Main( string[] args)
    {
        Application.Run(new frmApp());
    }
}