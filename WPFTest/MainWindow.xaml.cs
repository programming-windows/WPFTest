using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;

using Utils;

using WPFTest.UI.Menu;
using WPFTest.UI.Index;
using WPFTest.UI.Chapter1;
using WPFTest.UI.Chapter2;
using WPFTest.UI.Chapter3;
using WPFTest.UI.Chapter4;
using WPFTest.UI.Chapter5;
using WPFTest.UI.Chapter6;

namespace WPFTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public HeadPage headPage = new HeadPage();
        public MainMenuPage mainMenuPage = new MainMenuPage();

        public MainWindow()
        {

            InitializeComponent();

            //定义委托事件的lamda实现函数
            headPage.NextEvent += ((object arg) =>
            {
                MessageBox.Show("正在开发中......");
            });
            headPage.QuitEvent += ((object arg) =>
            {
                this.Close();
            });
            

            headPage.parentWindow = this;
            headFrame.Content = headPage;



            mainMenuPage.parentWindow = this;
            mainMenuFrame.Content = mainMenuPage;

            /*
            try
            {
                SqlserverUtil dbutil = new SqlserverUtil();
                SqlConnection connection = dbutil.connect();
                dbutil.close(connection);
            }
            catch (Exception)
            {

                System.Windows.MessageBox.Show("数据库连接异常,请检查配置", "警告");
                this.Close();
            }
             */
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

        }



        public void createLeftMenuPage(string topMenuName)
        {
            Page page = null;

            if ("index".Equals(topMenuName))
                page = createPageIndex();
            else if ("fgbz".Equals(topMenuName))
                page = createPageFgbz();
            else if ("chapter1".Equals(topMenuName))
                page = createPageChapter1();
            else if ("chapter2".Equals(topMenuName))
                page = createPageChapter2();
            else if ("chapter3".Equals(topMenuName))
                page = createPageChapter3();
            else if ("chapter4".Equals(topMenuName))
                page = createPageChapter4();
            else if ("chapter5".Equals(topMenuName))
                page = createPageChapter5();
            else if ("chapter6".Equals(topMenuName))
                page = createPageChapter6();
            else
            {
                MessageBox.Show("正在开发中......");
                return;
            }

            leftMenuFrame.Content = page;
        }


        private Page createPageChapter1()
        {
            MenuPageChapter1 page = new MenuPageChapter1();
            page.NextEvent += ((object arg) =>
            {
                if (arg.Equals("chapter1_sy1"))
                {
                    contentFrame.Content = createPageChapter1_sy1();
                }
                else if (arg.Equals("chapter1_sy2"))
                {
                    contentFrame.Content = createPageChapter1_sy2();
                }
                else
                {
                    MessageBox.Show("正在开发中......");
                    contentFrame.Content = createPageChapter1_sy1();
                }

            });

            return page;
        }


        private Page createPageChapter1_sy1()
        {
            C1_SY1 page = new C1_SY1();
            return page;
        }

        private Page createPageChapter1_sy2()
        {
            C1_SY2 page = new C1_SY2();
            


            return page;
        }


        private Page createPageChapter2()
        {
            MenuPageChapter2 page = new MenuPageChapter2();
            page.NextEvent += ((object arg) =>
            {
                if (arg.Equals("chapter2_sy1"))
                {
                    contentFrame.Content = createPageChapter2_sy1();
                }
                else if (arg.Equals("chapter2_sy2"))
                {
                    contentFrame.Content = createPageChapter2_sy2();
                }
                else
                {
                    MessageBox.Show("正在开发中......");
                    contentFrame.Content = createPageChapter2_sy1();
                }

            });

            return page;
        }

        private Page createPageChapter2_sy1()
        {
            C2_SY1 page = new C2_SY1();
            return page;
        }

        private Page createPageChapter2_sy2()
        {
            C2_SY2 page = new C2_SY2();
            return page;
        }


        private Page createPageChapter3()
        {
            MenuPageChapter3 page = new MenuPageChapter3();
            page.NextEvent += ((object arg) =>
            {
                if (arg.Equals("chapter3_sy1"))
                {
                    contentFrame.Content = createPageChapter3_sy1();
                }
                else if (arg.Equals("chapter3_sy2"))
                {
                    contentFrame.Content = createPageChapter3_sy2();
                }
                else if (arg.Equals("chapter3_sy3"))
                {
                    contentFrame.Content = createPageChapter3_sy3();
                }
                else
                {
                    MessageBox.Show("正在开发中......");
                    contentFrame.Content = createPageChapter3_sy1();
                }

            });

            return page;
        }

        private Page createPageChapter3_sy1()
        {
            C3_SY1 page = new C3_SY1();
            return page;
        }

        private Page createPageChapter3_sy2()
        {
            C3_SY2 page = new C3_SY2();
            return page;
        }

        private Page createPageChapter3_sy3()
        {
            C3_SY3 page = new C3_SY3();
            return page;
        }

        private Page createPageChapter4()
        {
            MenuPageChapter4 page = new MenuPageChapter4();
            page.NextEvent += ((object arg) =>
            {
                if (arg.Equals("chapter4_sy1"))
                {
                    contentFrame.Content = createPageChapter4_sy1();
                }
                else if (arg.Equals("chapter4_sy2"))
                {
                    contentFrame.Content = createPageChapter4_sy2();
                }
                else if (arg.Equals("chapter4_sy3"))
                {
                    contentFrame.Content = createPageChapter4_sy3();
                }
                else if (arg.Equals("chapter4_sy4"))
                {
                    contentFrame.Content = createPageChapter4_sy4();
                }
                else if (arg.Equals("chapter4_sy5"))
                {
                    contentFrame.Content = createPageChapter4_sy5();
                }
                else
                {
                    MessageBox.Show("正在开发中......");
                    contentFrame.Content = createPageChapter4_sy1();
                }

            });

            return page;
        }

        private Page createPageChapter4_sy1()
        {
            C4_SY1 page = new C4_SY1(this);
            page.NextEvent += ((object arg) =>
            {
                page.update(arg);
            });
            return page;
        }

        private Page createPageChapter4_sy2()
        {
            C4_SY2 page = new C4_SY2(this);
            return page;
        }

        private Page createPageChapter4_sy3()
        {
            C4_SY3 page = new C4_SY3(this);
            return page;
        }

        private Page createPageChapter4_sy4()
        {
            C4_SY4 page = new C4_SY4(this);
            return page;
        }

        private Page createPageChapter4_sy5()
        {
            C4_SY5 page = new C4_SY5(this);
            return page;
        }


        private Page createPageChapter5()
        {
            MenuPageChapter5 page = new MenuPageChapter5();
            page.NextEvent += ((object arg) =>
            {
                if (arg.Equals("chapter5_sy1"))
                {
                    contentFrame.Content = createPageChapter5_sy1();
                }
                else if (arg.Equals("chapter5_sy2"))
                {
                    contentFrame.Content = createPageChapter5_sy2();
                }
                else
                {
                    MessageBox.Show("正在开发中......");
                    contentFrame.Content = createPageChapter5_sy1();
                }

            });

            return page;
        }

        private Page createPageChapter5_sy1()
        {
            C5_SY1 page = new C5_SY1(this);
            return page;
        }

        private Page createPageChapter5_sy2()
        {
            C5_SY2 page = new C5_SY2(this);
            return page;
        }


        private Page createPageChapter6()
        {
            MenuPageChapter6 page = new MenuPageChapter6();
            page.NextEvent += ((object arg) =>
            {
                if (arg.Equals("chapter6_sy1"))
                {
                    contentFrame.Content = createPageChapter6_sy1();
                }
                else if (arg.Equals("chapter6_sy2"))
                {
                    contentFrame.Content = createPageChapter6_sy2();
                }
                else if (arg.Equals("chapter6_sy3"))
                {
                    contentFrame.Content = createPageChapter6_sy3();
                }
                else
                {
                    MessageBox.Show("正在开发中......");
                    contentFrame.Content = createPageChapter6_sy1();
                }

            });

            return page;
        }

        private Page createPageChapter6_sy1()
        {
            FileListPage page = new FileListPage();
            return page;
        }

        private Page createPageChapter6_sy2()
        {
            RoomTypeListPage page = new RoomTypeListPage();
            return page;
        }

        private Page createPageChapter6_sy3()
        {
            ADOTest page = new ADOTest(this);
            return page;
        }

        private Page createPageIndex()
        {
            MenuPageIndex page = new MenuPageIndex();
            page.NextEvent += ((object arg) =>
            {
                if (arg.Equals("index_jz"))
                {
                    contentFrame.Content = createPageIndex_Jz();
                }
                else
                {
                    MessageBox.Show("正在开发中......");
                    contentFrame.Content = createPageIndex_Jz();
                }
                /*
                else if (arg.Equals("index_sbdss"))
                {
                    contentFrame.Content = createPageIndex_sbdss() 
                }
                else if (arg.Equals("index_hss"))
                {
                    contentFrame.Content = createPageIndex_hss() 
                }
                else if (arg.Equals("index_hfw"))
                {
                    contentFrame.Content = createPageIndex_hfw() 
                }
                else if (arg.Equals("index_shdw"))
                {
                    contentFrame.Content = createPageIndex_shdw();
                }
                else if (arg.Equals("index_shry"))
                {
                    contentFrame.Content = createPageIndex_shry();
                }
                 * */

            });

            return page;
        }


        private Page createPageIndex_Jz()
        {
            JzListPage page = new JzListPage();
            return page;

        }


        private Page createPageFgbz()
        {
            MenuPageChapter6 page = new MenuPageChapter6();
            page.NextEvent += ((object arg) =>
            {
                if (arg.Equals("fgbz_fg"))
                {
                    contentFrame.Content = createPageFgbz_fg();
                }
                else
                {
                    MessageBox.Show("正在开发中......");
                    contentFrame.Content = createPageFgbz_fg();
                }
                /*
                else if (arg.Equals("index_sbdss"))
                {
                    contentFrame.Content = createPageIndex_sbdss() 
                }
                else if (arg.Equals("index_hss"))
                {
                    contentFrame.Content = createPageIndex_hss() 
                }
                else if (arg.Equals("index_hfw"))
                {
                    contentFrame.Content = createPageIndex_hfw() 
                }
                else if (arg.Equals("index_shdw"))
                {
                    contentFrame.Content = createPageIndex_shdw();
                }
                else if (arg.Equals("index_shry"))
                {
                    contentFrame.Content = createPageIndex_shry();
                }
                 * */

            });

            return page;
        }


        private Page createPageFgbz_fg()
        {
            FileListPage page = new FileListPage();
            return page;
        }
    }

    

     
}
