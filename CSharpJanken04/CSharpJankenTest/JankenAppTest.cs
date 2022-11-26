using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpJanken;

namespace CSharpJankenTest
{
    /// <summary>
    /// <see cref="JankenApp"/> クラスの単体テストを行います。
    /// </summary>
    [TestClass]
    public class JankenAppTest
    {
        /// <summary>
        /// <see cref="JankenApp.JudgeResult(int, int)"/> メソッドの単体テストです。
        /// </summary>
        [TestMethod]
        public void TestJudgeResult()
        {
            // JudgeResult メソッドは、あなたの手 3 通り、コンピュータの手 3 通り、
            // 合計 3 x 3 = 9 通りの組み合わせの中から結果を判断します。そのそれぞれの場合に
            // ついてテストを実施します。1 つのテストは、メソッドの入力、予期する結果、説明、
            // という形にまとめます。
            CheckJudgeResult(JankenApp.GU, JankenApp.GU, JankenApp.Draw, "GU, GU => Draw");
            CheckJudgeResult(JankenApp.GU, JankenApp.CYOKI, JankenApp.YouWin, "GU, CYOKI => YouWin");
            CheckJudgeResult(JankenApp.GU, JankenApp.PA, JankenApp.ComputerWin, "GU, PA => ComputerWin");

            CheckJudgeResult(JankenApp.CYOKI, JankenApp.GU, JankenApp.ComputerWin, "CYOKI, GU => ComputerWin");
            CheckJudgeResult(JankenApp.CYOKI, JankenApp.CYOKI, JankenApp.Draw, "CYOKI, CYOKI => Draw");
            CheckJudgeResult(JankenApp.CYOKI, JankenApp.PA, JankenApp.YouWin, "CYOKI, PA => YouWin");

            CheckJudgeResult(JankenApp.PA, JankenApp.GU, JankenApp.YouWin, "PA, GU => YouWin");
            CheckJudgeResult(JankenApp.PA, JankenApp.CYOKI, JankenApp.ComputerWin, "PA, CYOKI => ComputerWin");
            CheckJudgeResult(JankenApp.PA, JankenApp.PA, JankenApp.Draw, "PA, PA => Draw");
        }

        /// <summary>
        /// 指定の場合について <see cref="JankenApp.JudgeResult(int, int)"/> メソッドの動作を確認します。
        /// </summary>
        /// <param name="yourHand">あなたの手です。</param>
        /// <param name="computerHand">コンピュータの手です。</param>
        /// <param name="expectedResult">予期する結果です。</param>
        /// <param name="message">テストの説明です。</param>
        private void CheckJudgeResult(
            int yourHand, int computerHand, int expectedResult, string message)
        {
            // 指定の引数でメソッドを呼び出し、実際の結果が予期するものと同じかどうか確認します。
            int actualResult = JankenApp.JudgeResult(yourHand, computerHand);
            Assert.AreEqual(expectedResult, actualResult, message);
        }
    }
}
