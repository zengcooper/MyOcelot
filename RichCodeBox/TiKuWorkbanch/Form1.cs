using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiKuWorkbanch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("题号", typeof(string));
            dt.Columns.Add("题干", typeof(string));
            dt.Columns.Add("参考答案", typeof(string));
            dt.Columns.Add("分值", typeof(string));
            dt.Columns.Add("基础题型", typeof(string));
            dt.Columns.Add("题干类别", typeof(string));

            string strText =
                @"I moved to New York City after college.I quickly learned that smiling, for the most part, was not a good thing.A smile attracted__41__attention, especially from men who would stop me as I walked down the street.Being an inexperienced young woman, I didn’t know how to deal with such__42__.I felt powerless，__43__， and disturbed.__44__I found a kind of neutral(中立的)__45__that I wore every time I went outside.If I__46__at a stranger, it was forced, a polite return of a smile.My true smile came out only for my loved ones.
 One day, in a book store I__47__the story that changed my outlook about the way I looked as I walked through the world—The Smile, which__48__“The Smile was a magic moment when two souls recognize each other.” I__49__that moment too because I had experienced it several times and to me it felt so full of__50__and possibility.The next morning on my way to work I stopped in a cafe and bought a cup of tea.When the__51__handed me the cup I smiled and said thank you.He__52__back and for a moment looked__53__.Then he smiled and said, “I’ve been doing this all morning，__54__you’re the first person to smile at me.” I just__55__， smiled again and left.
 I couldn’t__56__how good I felt—I actually felt more like myself! As I continued to smile throughout the days, weeks and months I noticed an amazing__57__：The more I smiled, the__58__everything seemed; and the friendlier everything seemed, the more I smiled.It also seemed to me that the smiles I received__59__were not just polite smiles.I felt a momentary__60__with the person, as though we had come to an agreement that all was right with the world.
41．A.unwanted  B．informal   C．illegal  D．impossible
42．A.change  B．behavior  C．decision  D．belief
43．A.surprised  B．annoyed  C．satisfied  D．shocked
44．A.Equally  B．Actually  C．Frequently  D．Eventually
45．A.clothes  B．shoes  C．expression  D．glasses
46．A.smiled  B．glared  C．stared  D．laughed
47．A.thought of  B．came across  C．wrote down  D．came up with
48．A.wrote  B．spoke  C．read  D．told
49．A.purchased  B．pleased  C．improved  D．recognized
50．A.hardship  B．symptoms  C．sadness  D．hope
51．A.waitress  B．customer  C．waiter  D．writer
52．A.ran  B．pushed  C．stepped  D．paid
53．A.scared  B．confused  C．amused  D．concerned
54．A.but  B．if  C．so  D．as
55．A.nodded  B．cried  C．shouted  D．shook
56．A.consider  B．believe  C．doubt  D．understand
57．A.story  B．lie  C．cycle  D．idea
58．A.stronger  B．rarer  C．crueler  D．friendlier
59．A.on time  B．in turn  C．in contrast  D．in return
60．A.connection  B．consultation  C．conversation  D．argument
";
            Regex exp = new Regex(@"[\s]+[1-9][0-9]{1,2}[．.].+");
            MatchCollection matchCollection = exp.Matches(strText);

            foreach (Match item in matchCollection)
            {
                //题号
                string strNO = Regex.Match(item.Value, @"[\s]+[1-9][0-9]{1,2}[．.]").Value.Trim('．', '.');

                DataRow dr = dt.NewRow();
                dr["题号"] = "第" + strNO + "小题";
                dr["题干"] = item.Value;
                dr["参考答案"] = "A";
                dr["分值"] = 1;
                dr["基础题型"] = "单选题";
                dr["题干类别"] = "单项选择题";
                dt.Rows.Add(dr);
            }



            dataGridView1.DataSource = dt;
            //dataGridView1.AutoGenerateColumns = false;

        }
    }
}
