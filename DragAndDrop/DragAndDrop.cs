using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace DragAndDrop
{
    class DragAndDrop
    {
        /* 以下の処理をドラッグアンドドロップを実装したいフォームに上書きし、適宜変更すること
        public Form1()
        {
            InitializeComponent();

            // ドラッグアンドドロップの処理を実装
            new DragAndDrop(this.textBox1, AfterDragAndDrop);
        }
        private void AfterDragAndDrop(string[] strAllFilePath)
        {
            foreach(string strFilePath in strAllFilePath)
            {
                // ドラッグアンドドロップされた各ファイルに対して処理を行う

                // ドラッグアンドドロップ後の処理を記述
                MessageBox.Show(strFilePath);
            }
        }
         * */

        /// <summary>
        /// ドラッグアンドドロップされた後の処理
        /// </summary>
        Action<string[]> actAfterDropped;

        /// <summary>
        /// ドラッグアンドドロップの処理
        /// </summary>
        /// <param name="objTargetInstance">FormやTextboxなど、ドラッグアンドドロップを実装したいオブジェクトのインスタンス</param>
        /// <param name="actMethod">ドラッグアンドドロップされたファイル名の配列を受け取り、処理を行うメソッド</param>
        public DragAndDrop(object objTargetInstance, Action<string[]> actMethod)
        {
            // 対象のオブジェクトに対してアイテムをドロップ出来るようにする
            objTargetInstance.GetType().GetProperty("AllowDrop").SetValue(objTargetInstance, true);

            // 対象のオブジェクトにこのクラス内のイベントハンドラを設定
            SetEventHandler(objTargetInstance, "DragEnter", this, "DragEnter");
            SetEventHandler(objTargetInstance, "DragDrop", this, "DragDrop");

            actAfterDropped = actMethod;
        }

        /// <summary>
        /// イベントハンドラの設定
        /// </summary>
        /// <param name="objTargetInstance">イベントハンドラを実装するオブジェクトのインスタンス</param>
        /// <param name="strEvent">オブジェクトのインスタンス内に存在するイベントハンドラの名前</param>
        /// <param name="objTargetMethodInstance">イベントハンドラに登録するメソッドがあるクラスのインスタンス</param>
        /// <param name="strTargetMethod">イベントハンドラに登録するメソッド名</param>
        private void SetEventHandler(object objTargetInstance, string strEvent, object objTargetMethodInstance, string strTargetMethod)
        {
            // コントロール内のイベントを取得
            EventInfo evInfo = objTargetInstance.GetType().GetEvent(strEvent);

            // デリゲートを作成
            Delegate handler = Delegate.CreateDelegate(evInfo.EventHandlerType, objTargetMethodInstance, strTargetMethod);
            
            // イベントの設定
            evInfo.AddEventHandler(objTargetInstance, handler);
        }

        /// <summary>
        /// アイテムを進入させた時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DragEnter(object sender, DragEventArgs e)
        {
            // カーソルの形を変える
            e.Effect = DragDropEffects.All;
        }

        /// <summary>
        /// アイテムをドロップした時の動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ドロップされたアイテムがファイルだった場合のみ処理をする

                // ドラッグアンドドロップした後の処理
                actAfterDropped((string[])e.Data.GetData(DataFormats.FileDrop, false));
            }
        }
    }
}
