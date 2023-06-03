using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpJanken;

namespace CSharpJankenTest
{
    /// <summary>
    /// <see cref="JankenHand"/> クラスの単体テストを行います。
    /// </summary>
    [TestClass]
    public class JankenHandTest
    {
        /// <summary>
        /// <see cref="JankenHand.FromString(string)"/> メソッドの単体テストです。
        /// </summary>
        [TestMethod]
        public void TestFromString()
        {
            CheckFromString("グー", JankenHand.GU, "'GU' => グー");
            CheckFromString("チョキ", JankenHand.CYOKI, "'チョキ' => チョキ");
            CheckFromString("パー", JankenHand.PA, "'パー' => パー");

            CheckFromString("GU", null, "手を表わす文字列でない => null");
            CheckFromString(null, null, "null => null");
            CheckFromString(string.Empty, null, "'' => null");
        }

        /// <summary>
        /// 指定の場合について <see cref="JankenHand.FromString(string)"/> メソッドの動作を確認します。
        /// </summary>
        /// <param name="s">FromString メソッドに渡す引数です。</param>
        /// <param name="expectedHand">予期する結果です。</param>
        /// <param name="message">テストの説明です。</param>
        private void CheckFromString(string s, JankenHand expectedHand, string message)
        {
            // 指定の引数でメソッドを呼び出し、実際の手が予期するものと同じかどうか確認します。
            JankenHand actualHand = JankenHand.FromString(s);
            Assert.AreSame(expectedHand, actualHand, message);
        }

        /// <summary>
        /// <see cref="JankenHand"/> のインスタンスを <see cref="JankenHand.ToString"/> して、
        /// その結果を <see cref="JankenHand.FromString(string)"/> すると、もとに戻る。
        /// </summary>
        [TestMethod]
        public void TestToFromString()
        {
            CheckToFromString(JankenHand.GU, "グー");
            CheckToFromString(JankenHand.CYOKI, "チョキ");
            CheckToFromString(JankenHand.PA, "パー");
        }

        /// <summary>
        /// 指定の手について、<see cref="JankenHand.ToString"/> して、その結果を
        /// <see cref="JankenHand.FromString(string)"/> すると、もとに戻ることを確認します。
        /// </summary>
        /// <param name="hand">テストする手</param>
        /// <param name="name">テストする手の名前</param>
        private void CheckToFromString(JankenHand hand, string name)
        {
            string s = hand.ToString();
            JankenHand handToFrom = JankenHand.FromString(s);
            Assert.AreSame(hand, handToFrom, $"{name}: ToString して FromString すると元に戻る");
        }

        /// <summary>
        /// <see cref="JankenHand.JudgeResult(JankenHand, JankenHand)"/> メソッドの単体テストです。
        /// </summary>
        [TestMethod]
        public void TestJudgeResult()
        {
            // JudgeResult メソッドは、あなたの手 3 通り、コンピュータの手 3 通り、
            // 合計 3 x 3 = 9 通りの組み合わせの中から結果を判断します。そのそれぞれの場合に
            // ついてテストを実施します。1 つのテストは、メソッドの入力、予期する結果、説明、
            // という形にまとめます。
            CheckJudgeResult(JankenHand.GU, JankenHand.GU, JankenApp.Draw, "GU, GU => Draw");
            CheckJudgeResult(JankenHand.GU, JankenHand.CYOKI, JankenApp.YouWin, "GU, CYOKI => YouWin");
            CheckJudgeResult(JankenHand.GU, JankenHand.PA, JankenApp.ComputerWin, "GU, PA => ComputerWin");

            CheckJudgeResult(JankenHand.CYOKI, JankenHand.GU, JankenApp.ComputerWin, "CYOKI, GU => ComputerWin");
            CheckJudgeResult(JankenHand.CYOKI, JankenHand.CYOKI, JankenApp.Draw, "CYOKI, CYOKI => Draw");
            CheckJudgeResult(JankenHand.CYOKI, JankenHand.PA, JankenApp.YouWin, "CYOKI, PA => YouWin");

            CheckJudgeResult(JankenHand.PA, JankenHand.GU, JankenApp.YouWin, "PA, GU => YouWin");
            CheckJudgeResult(JankenHand.PA, JankenHand.CYOKI, JankenApp.ComputerWin, "PA, CYOKI => ComputerWin");
            CheckJudgeResult(JankenHand.PA, JankenHand.PA, JankenApp.Draw, "PA, PA => Draw");
        }

        /// <summary>
        /// 指定の場合について <see cref="JankenApp.JudgeResult(int, int)"/> メソッドの動作を確認します。
        /// </summary>
        /// <param name="yourHand">あなたの手です。</param>
        /// <param name="computerHand">コンピュータの手です。</param>
        /// <param name="expectedResult">予期する結果です。</param>
        /// <param name="message">テストの説明です。</param>
        private void CheckJudgeResult(
            JankenHand yourHand, JankenHand computerHand, int expectedResult, string message)
        {
            // 指定の引数でメソッドを呼び出し、実際の結果が予期するものと同じかどうか確認します。
            int actualResult = JankenHand.JudgeResult(yourHand, computerHand);
            Assert.AreEqual(expectedResult, actualResult, message);
        }
    }
}
