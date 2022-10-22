// C# でじゃんけんするプログラムです。
// あなたとコンピュータでじゃんけんをします。

// 'System.Console' などで 'System' を省略し、単に 'Console' と書けるようにします。
using System;

// この中で定義した名前が格納される名前空間を指定します。
// この中で定義した名前が、他で定義されたものと重ならないようにします。
namespace CSharpJanken
{
    // クラス名は、じゃんけんアプリケーションということで JankenApp にします。
    class JankenApp
    {
        // じゃんけんで出す手、グー、チョキ、パーは、整数の 0、1、2 で表わすことにします。
        private const int GU = 0;
        private const int CYOKI = 1;
        private const int PA = 2;

        // コンピュータが出す手は乱数で生成します。そのときの最大値です。
        private const int HAND_MAX = 3;

        // メインメソッドです。実行の流れは次のようになります。
        // 1. あなたが出す手を読み込みます。
        // 2. コンピュータが出す手を乱数で生成します。
        // 3. あなたの手とコンピュータの手を表示します。
        // 4. 勝ち、負け、あいこを判断して表示します。
        static void Main()
        {
            // コンピュータが出す手の生成に使う Random クラスのインスタンスを作成しておきます。
            Random rand = new Random();

            Console.WriteLine("じゃんけんしましょう！");

            // 1. あなたが出す手を読み込みます。
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

            // 2. コンピュータが出す手を乱数で生成します。
            // 乱数は System.Random.Next(Int32) メソッドで生成します。このメソッドは 0 以上、
            // 指定の最大値未満のランダムな整数を返します。ここでは、0, 1, 2 のいずれかを返します。
            // 返された値は、整数型の変数 'computerHand' に格納します。
            int computerHand = rand.Next(HAND_MAX);

            // 3. あなたの手とコンピュータの手を表示します。
            Console.WriteLine("あなたの手:       {0}", yourHand);
            Console.WriteLine("コンピュータの手: {0}", computerHand);

            // 4. 勝ち、負け、あいこを判断して表示します。
            // あなたの手は GU、CYOKI、PA の 3 つのうちのどれか、コンピュータの手も
            // その 3 つのうちのどれかです。あなたの手とコンピュータの手の組み合わせは
            // 3 x 3 = 9 通りのうちのどれかになります。これらを if 文で場合分けし、
            // 勝ち、負け、あいこを判断し、その結果を表示します。
            if (yourHand == GU)
            {
                if (computerHand == GU)
                {
                    // あなた: GU、コンピュータ: GU => あいこ
                    Console.WriteLine("あいこです。");
                }
                else if (computerHand == CYOKI)
                {
                    // あなた: GU、コンピュータ: CYOKI => あなたの勝ち
                    Console.WriteLine("あなたの勝ちです。");
                }
                else
                {
                    // あなた: GU、コンピュータ: PA => コンピュータの勝ち
                    Console.WriteLine("コンピュータの勝ちです。");
                }
            }
            else if (yourHand == CYOKI)
            {
                if (computerHand == GU)
                {
                    // あなた: CYOKI、コンピュータ: GU => コンピュータの勝ち
                    Console.WriteLine("コンピュータの勝ちです。");
                }
                else if (computerHand == CYOKI)
                {
                    // あなた: CYOKI、コンピュータ: CYOKI => あいこ
                    Console.WriteLine("あいこです。");
                }
                else
                {
                    // あなた: CYOKI、コンピュータ: PA => あなたの勝ち
                    Console.WriteLine("あなたの勝ちです。");
                }
            }
            else
            {
                if (computerHand == GU)
                {
                    // あなた: PA、コンピュータ: GU => あなたの勝ち
                    Console.WriteLine("あなたの勝ちです。");
                }
                else if (computerHand == CYOKI)
                {
                    // あなた: PA、コンピュータ: CYOKI => コンピュータの勝ち
                    Console.WriteLine("コンピュータの勝ちです。");
                }
                else
                {
                    // あなた: PA、コンピュータ: PA => あいこ
                    Console.WriteLine("あいこです。");
                }
            }

            Console.WriteLine("また遊びましょう。それでは！");
        }
    }
}
