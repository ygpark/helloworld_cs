using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDynamicDll
{
	public delegate string SayDel(string sData);

	public partial class Form1 : Form
	{
		

		private object m_Type = null;

		public Form1()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Dll을 로드 하는 클래스
		/// </summary>
		private bool LoadDll(int nDll)
		{
			//DLL 이름
			string sFileName = "";

			switch(nDll)
			{
				case 1:
					sFileName = "TestDll1.dll";
					break;
				case 2:
					sFileName = "TestDll2.dll";
					break;

				default:	//에러
					return false;
			}

			//Dll이 있는지 여부
			if( false == File.Exists(sFileName) )
			{	//없으면 에러
				return false;
			}

			//Dll 로드
			Assembly asm = Assembly.LoadFrom(sFileName);

			//로드가 되었나?
			if (asm == null)
			{
				return false;
			}

			//로드가되었으면 
			//Dll에 소속된 구성요소 리스트를 받아온다.
			Type[] types = asm.GetExportedTypes();

			//types[]의 내용으로 원하는 네임스페이스나 클래스를 찾을수 있다.
			//이 예제에서는 네임스페이스와 클래스가 한개뿐이기 때문에 그냥 0번 오브젝트를 사용한다.
			m_Type = Activator.CreateInstance(types[0]);
			

			//Dll로드가 정상적으로 끝났다.
			return true;

		}

		private void DllOutText()
		{
			//오브젝트 안의 "Say"메소드를 찾는다.
			MethodInfo methodInfo = m_Type.GetType().GetMethod("Say");
			
			//파라메타를 보네 메소드를 호출한다.
			txtOut.Text = methodInfo.Invoke(m_Type, new object[] { "호출" }).ToString();

		}

		private void DllOutText_Del()
		{

			MethodInfo minfo = m_Type.GetType().GetMethod("Say");
			SayDel delSay
				= (SayDel)Delegate.CreateDelegate(typeof(SayDel), null, minfo);
			txtOut.Text = delSay("호출");

		}

		private void btnDll1_Click(object sender, EventArgs e)
		{
			if (true == LoadDll(1))
			{
				DllOutText();
			}
		}

		private void btnDll2_Click(object sender, EventArgs e)
		{
			if (true == LoadDll(2))
			{
				DllOutText();
			}
		}

		private void btnDll1_Del_Click(object sender, EventArgs e)
		{
			if (true == LoadDll(1))
			{
				DllOutText_Del();
			}
		}

		private void btnDll2_Del_Click(object sender, EventArgs e)
		{
			if (true == LoadDll(2))
			{
				DllOutText_Del();
			}
		}
	}
}
