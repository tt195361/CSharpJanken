// 使用する名前空間を宣言します。これらの名前空間で定義された名称は、名前空間を省略して
// 参照できるようになります。
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpJanken
{
    /// <summary>
    /// じゃんけんの手を表わすクラスです。
    /// </summary>
    internal class JankenHand
    {
        // じゃんけんの手を表わすクラスを作成し グー、チョキ、パーをそのインスタンスにします。
        // じゃんけんの手ををクラスにすることで、メンバー変数、プロパティ、メソッドなど
        // が定義できるようになります。ここでは、以下のようにします。
        //   - メンバー変数で、それぞれの手がどの手に勝つかを保持します。
        //   - 勝ち、負け、あいこを判断するメソッドで、上記のどの手に勝つかの情報を
        //     使います。
        //   - ToString() を書き換えて、手を表わす文字列を返すようにします。
        //   - 手を表わす文字列に対応するじゃんけんの手を返すメソッド FromString(string) を。
        //     作ります。
        //
        // グー、チョキ、パーを数字や enum で表わし、条件分岐や
        // 配列などのデータ構造を用いてどの手に勝つかを決める方法にくらべて、
        // クラスにしてメンバー変数やメソッドを使うほうが、使用する情報が
        // クラスの内部にまとまり、クラス外部とのインターフェースもすっきりすると思います。
        //
        // プログラムは、以下のように実装します。
        //   - それぞれのインスタンスは JankenHand クラスの static なプロパティで
        //     取得できるようにします。static なプロパティはクラス名を使って
        //     JankenHand.GU, JankenHand.CYOKI, JankenHand.PA のようにアクセスできます。
        //   - それぞれのインスタンスは、クラスを初期化する static のコンストラクタで
        //     作成します。static コンストラクタは、明示的にはどこからも呼んでいませんが、
        //     プログラム実行で、そのクラスへの最初のアクセス時に実行されます。
        //   - インスタンスのコンストラクタは、可視範囲を private にして、このクラスの
        //     外からは new でインスタンスが生成できないようにします。

        /// <summary>
        /// じゃんけんのグーを表わすインスタンスを取得します。
        /// </summary>
        internal static JankenHand GU { get; }

        /// <summary>
        /// じゃんけんのチョキを表わすインスタンスを取得します。
        /// </summary>
        internal static JankenHand CYOKI { get; }

        /// <summary>
        /// じゃんけんのパーを表わすインスタンスを取得します。
        /// </summary>
        internal static JankenHand PA { get; }

        /// <summary>
        /// すべての手を格納するコレクションを取得します。プログラムの他の部分から内容を変更できなく
        /// するため、変更不可の <see cref="ReadOnlyCollection{T}"/> にします。
        /// </summary>
        internal static ReadOnlyCollection<JankenHand> AllHands { get; }

        /// <summary>
        /// 手の数を取得します。
        /// </summary>
        internal static int NUM_HANDS { get; }

        /// <summary>
        /// クラスを初期化します。ここでは必要なインスタンスを作成し、
        /// それらを初期設定します。
        /// </summary>
        static JankenHand()
        {
            // それぞれのインスタンスを作成します。
            GU = new JankenHand("グー");
            CYOKI = new JankenHand("チョキ");
            PA = new JankenHand("パー");

            // それぞれの手に、その手が勝つ手を設定します。
            // "グー" は "チョキ" に、"チョキ"　は "パー" に、"パー" は "グー" に勝ちます。
            // この設定をコンストラクタで "new JankenHand("グー", CYOKI)" のように
            // できればいいのですが、最初に GU を作成する時点では、まだ CYOKI はなく、、、
            // となっているので、コンストラクタでは設定できません。そのため、すべての
            // インスタンスを作成してからそれぞれに設定しています。
            GU.Wins = CYOKI;
            CYOKI.Wins = PA;
            PA.Wins = GU;

            AllHands = new ReadOnlyCollection<JankenHand>(new JankenHand[] { GU, CYOKI, PA });
            NUM_HANDS = AllHands.Count;
        }

        /// <summary>
        /// 指定の文字列に対応するじゃんけんの手を返します。
        /// </summary>
        /// <param name="s">じゃんけんの手を表わす文字列です。</param>
        /// <returns>
        /// 指定の文字列に対応するじゃんけんの手を返します。
        /// 対応する手がない場合は <see langword="null"/> を返します。
        /// </returns>
        internal static JankenHand FromString(string s)
        {
            // すべての手の名前から指定の文字列に一致するものを見つけます。なければ null を返します。
            JankenHand foundHand = AllHands.Where((h) => (h.Name == s)).FirstOrDefault();
            return foundHand;

            // foreach ループだと次のようになります。
            //JankenHand foundHand = null;
            //foreach (JankenHand hand in AllHands)
            //{
            //    if (hand.Name == s)
            //    {
            //        foundHand = hand;
            //        break;
            //    }
            //}
            //return foundHand;
        }

        /// <summary>
        /// 勝ち、負け、あいこを判断します。
        /// </summary>
        /// <param name="yourHand">あなたの手です。</param>
        /// <param name="computerHand">コンピュータの手です。</param>
        /// <returns>
        /// 判断した結果を返します。
        /// </returns>
        internal static int JudgeResult(JankenHand yourHand, JankenHand computerHand)
        {
            int result;

            if (yourHand == computerHand)
            {
                // あなたの手とコンピュータの手が同じなら、あいこです。
                result = JankenApp.Draw;
            }
            else if (yourHand.Wins == computerHand)
            {
                // あなたの手が勝つ手がコンピュータの手と同じなら、あなたの勝ちです。
                result = JankenApp.YouWin;
            }
            else
            {
                // 上記以外なら、コンピュータの勝ちです。
                result = JankenApp.ComputerWin;
            }

            return result;
        }

        // ここまではスタティックの定義、ここからはインスタンスの定義です。

        /// <summary>
        /// 手の名前を取得します。
        /// </summary>
        internal string Name { get; }

        /// <summary>
        /// この手が勝つ手を設定、取得します。
        /// </summary>
        private JankenHand Wins { get; set; }

        /// <summary>
        /// 指定の引数で手を初期化します。
        /// </summary>
        /// <param name="name">手の名前です。</param>
        /// <remarks>
        /// クラスの外部からインスタンスが生成できないようにするため、
        /// 可視範囲を private にします。
        /// </remarks>
        private JankenHand(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 手を表わす文字列を取得します。
        /// </summary>
        /// <returns>手を表わす文字列を返します。</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
