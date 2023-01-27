// C# でじゃんけんするプログラム 第 5 版です。
// あなたとコンピュータでじゃんけんをします。
// 第 5 版では、勝ち、負け、あいこを判断するメソッド JudgeResult メソッドを
// さらに書き換えます。第 4 版では、手の組み合わせに対する結果をデータとして用意して
// おくようにしました。これを計算と比較により求めるように変更します。

// 'System.Console' などで 'System' を省略し、単に 'Console' と書けるようにします。
using System;
using System.Runtime.CompilerServices;

// この中で定義した名前が格納される名前空間を指定します。
// この中で定義した名前が、他で定義されたものと重ならないようにします。
// InternalsVisibleTo アトリビュートで、単体テストのプロジェクト CSharpJankenTest が
// このプロジェクトの internal の定義を使えるようにします。
[assembly: InternalsVisibleTo("CSharpJankenTest")]
namespace CSharpJanken
{
    // クラス名は、じゃんけんアプリケーションということで JankenApp にします。
    internal class JankenApp
    {
        // じゃんけんで出す手、グー、チョキ、パーは、整数の 0、1、2 で表わすことにします。
        internal const int GU = 0;
        internal const int CYOKI = 1;
        internal const int PA = 2;

        // コンピュータが出す手は乱数で生成します。そのときの最大値です。
        private const int HAND_MAX = 3;

        // 勝負の結果、あなたの勝ち、あいこ、コンピュータの勝ちを、整数の 1, 0, -1 で表わすことにします
        internal const int YouWin = 1;
        internal const int Draw = 0;
        internal const int ComputerWin = -1;

        // コンピュータが出す手の生成に使う Random クラスのインスタンスを作成しておきます。
        private static Random _rand = new Random();

        // メインメソッドです。
        internal static void Main()
        {
            Console.WriteLine("じゃんけんしましょう！");

            // 1. あなたが出す手を読み込みます。
            int yourHand = ReadYourHand();

            // 2. コンピュータが出す手を乱数で生成します。
            int computerHand = DecideComputerHand();

            // 3. あなたの手とコンピュータの手を表示します。
            ShowHands(yourHand, computerHand);

            // 4. 勝ち、負け、あいこを判断します。
            int result = JudgeResult(yourHand, computerHand);

            // 5. 結果を表示します。
            ShowResult(result);

            Console.WriteLine("また遊びましょう。それでは！");
        }

        /// <summary>
        /// あなたが出す手を読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだあなたが出す手を返します。
        /// </returns>
        internal static int ReadYourHand()
        {
            // あなたの手を格納する整数型の変数 'yourHand' を定義します。
            int yourHand;

            // あなたの手が読み込めるまで繰り返します。
            for ( ; ; )
            {
                // 入力を促すメッセージを表示します。
                string prompt = String.Format(
                    "あなたはなにを出しますか ({0}:グー、{1}:チョキ、{2}:パー): ",
                    GU, CYOKI, PA);
                Console.Write(prompt);

                // キーボードから入力された文字列を、変数 'yourInput' に格納します。
                string yourInput = Console.ReadLine();

                // 変数 'yourInput' に格納された文字列を整数として解釈し、うまく解釈できれば
                // 変数 'yourHand' に整数値を格納します。さらに 'yourHand' が GU、CYOHI、
                // あるいは PA なら、繰り返しから抜けます。
                if (Int32.TryParse(yourInput, out yourHand)
                    && (yourHand == GU || yourHand == CYOKI || yourHand == PA))
                {
                    break;
                }

                // ここが実行されるのは、入力された文字列が整数でなかった、あるいは
                // GU, CYOKI、PA のいずれでもなかった場合です。問題の内容とその対処方法を示す
                // メッセージを表示します。
                string errorMessage = String.Format(
                    "'{0}' の入力は不正です。{1}、{2}、{3} のいずれかを入力してください。",
                    yourInput, GU, CYOKI, PA);
                Console.WriteLine(errorMessage);
            }

            return yourHand;
        }

        /// <summary>
        /// コンピュータが出す手を乱数で生成します。
        /// </summary>
        /// <returns>
        /// 生成したコンピュータが出す手を返します。
        /// </returns>
        internal static int DecideComputerHand()
        {
            // 乱数は System.Random.Next(Int32) メソッドで生成します。このメソッドは 0 以上、
            // 指定の最大値未満のランダムな整数を返します。ここでは、0, 1, 2 のいずれかを返します。
            // 返された値は、整数型の変数 'computerHand' に格納します。
            int computerHand = _rand.Next(HAND_MAX);

            return computerHand;
        }

        /// <summary>
        /// あなたの手とコンピュータの手を表示します。
        /// </summary>
        /// <param name="yourHand">あなたの手です。</param>
        /// <param name="computerHand">コンピュータの手です。</param>
        internal static void ShowHands(int yourHand, int computerHand)
        {
            Console.WriteLine("あなたの手:       {0}", yourHand);
            Console.WriteLine("コンピュータの手: {0}", computerHand);
        }

        /// <summary>
        /// 勝ち、負け、あいこを判断します。
        /// </summary>
        /// <param name="yourHand">あなたの手です。</param>
        /// <param name="computerHand">コンピュータの手です。</param>
        /// <returns>
        /// 判断した結果を返します。
        /// </returns>
        internal static int JudgeResult(int yourHand, int computerHand)
        {
            int result;

            if (yourHand == computerHand)
            {
                // あなたの手とコンピュータの手が同じなら、あいこです。
                result = Draw;
            }
            else if ((yourHand + 1) % HAND_MAX == computerHand)
            {
                // (あなたの手 + 1) % HAND_MAX がコンピュータの手と等しいなら、あなたの勝ちです。
                // (GU (0)    + 1) % 3 = 1 % 3 = CYOKI (1)
                // (CYOKI (1) + 1) % 3 = 2 % 3 = PA (2)
                // (PA (2)    + 1) % 3 = 3 % 3 = GU (0)
                result = YouWin;
            }
            else
            {
                // 上記以外なら、コンピュータの勝ちです。
                result = ComputerWin;
            }

            return result;
        }

        /// <summary>
        /// 結果を表示します。
        /// </summary>
        /// <param name="result">判断した結果です。</param>
        internal static void ShowResult(int result)
        {
            if (result == YouWin)
            {
                Console.WriteLine("あなたの勝ちです。");
            }
            else if (result == Draw)
            {
                Console.WriteLine("あいこです。");
            }
            else
            {
                Console.WriteLine("コンピュータの勝ちです。");
            }
        }
    }
}
