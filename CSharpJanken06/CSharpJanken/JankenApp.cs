// C# でじゃんけんするプログラム 第 6 版です。
// あなたとコンピュータでじゃんけんをします。
// 第 6 版では、じゃんけんの手を表わす グー、チョキ、パーをクラスにします。
// 第 5 版までは、グー、チョキ、パーは、整数の 0、1、2 で表わしていました。

// 'System.Console' などで 'System' を省略し、単に 'Console' と書けるようにします。
using System;
using System.Runtime.CompilerServices;
using System.Linq;

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
            JankenHand yourHand = ReadYourHand();

            // 2. コンピュータが出す手を乱数で生成します。
            JankenHand computerHand = DecideComputerHand();

            // 3. あなたの手とコンピュータの手を表示します。
            ShowHands(yourHand, computerHand);

            // 4. 勝ち、負け、あいこを判断します。
            int result = JankenHand.JudgeResult(yourHand, computerHand);

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
        internal static JankenHand ReadYourHand()
        {
            // あなたの手を格納する JankenHand 型の変数 'yourHand' を定義します。
            JankenHand yourHand;

            // あなたの手が読み込めるまで繰り返します。
            for ( ; ; )
            {
                // それぞれの手を "、" で区切ったを文字列を作成し、それを使って入力を促す
                // メッセージを表示します。
                string allHandsStr = String.Join("、", JankenHand.AllHands);
                string prompt = $"あなたはなにを出しますか {allHandsStr}: ";
                Console.Write(prompt);

                // キーボードから入力された文字列を、変数 'yourInput' に格納します。
                string yourInput = Console.ReadLine();

                // 変数 'yourInput' に格納された文字列を引数として JankenHand.FromString メソッドを
                // 呼び出します。このメソッドは指定の文字列に対応する JankenHand クラスのインスタンスを
                // 返します。指定の文字列に対応するインスタンスがない場合は null が返されます。
                yourHand = JankenHand.FromString(yourInput);
                if (yourHand != null)
                {
                    break;
                }

                // ここが実行されるのは、入力された文字列に対応する JankenHand クラスのインスタンスが
                // なかった場合です。問題の内容とその対処方法を示すメッセージを表示します。
                string errorMessage = 
                    $"'{yourInput}' の入力は不正です。{allHandsStr} のいずれかを入力してください。";
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
        internal static JankenHand DecideComputerHand()
        {
            // 乱数は System.Random.Next(Int32) メソッドで生成します。このメソッドは 0 以上、
            // 指定の最大値未満のランダムな整数を返します。ここでは、0, 1, 2 のいずれかを返します。
            // 返された値は、整数型の変数 'computerHand' に格納します。
            int randomIndex = _rand.Next(JankenHand.NUM_HANDS);
            JankenHand computerHand = JankenHand.AllHands[randomIndex];

            return computerHand;
        }

        /// <summary>
        /// あなたの手とコンピュータの手を表示します。
        /// </summary>
        /// <param name="yourHand">あなたの手です。</param>
        /// <param name="computerHand">コンピュータの手です。</param>
        internal static void ShowHands(JankenHand yourHand, JankenHand computerHand)
        {
            Console.WriteLine($"あなたの手:       {yourHand}");
            Console.WriteLine($"コンピュータの手: {computerHand}");
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
