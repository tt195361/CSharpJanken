using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpJanken;

namespace CSharpJankenTest
{
    /// <summary>
    /// <see cref="JankenApp"/> �N���X�̒P�̃e�X�g���s���܂��B
    /// </summary>
    [TestClass]
    public class JankenAppTest
    {
        /// <summary>
        /// <see cref="JankenApp.JudgeResult(int, int)"/> ���\�b�h�̒P�̃e�X�g�ł��B
        /// </summary>
        [TestMethod]
        public void TestJudgeResult()
        {
            // JudgeResult ���\�b�h�́A���Ȃ��̎� 3 �ʂ�A�R���s���[�^�̎� 3 �ʂ�A
            // ���v 3 x 3 = 9 �ʂ�̑g�ݍ��킹�̒����猋�ʂ𔻒f���܂��B���̂��ꂼ��̏ꍇ��
            // ���ăe�X�g�����{���܂��B1 �̃e�X�g�́A���\�b�h�̓��́A�\�����錋�ʁA�����A
            // �Ƃ����`�ɂ܂Ƃ߂܂��B
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
        /// �w��̏ꍇ�ɂ��� <see cref="JankenApp.JudgeResult(int, int)"/> ���\�b�h�̓�����m�F���܂��B
        /// </summary>
        /// <param name="yourHand">���Ȃ��̎�ł��B</param>
        /// <param name="computerHand">�R���s���[�^�̎�ł��B</param>
        /// <param name="expectedResult">�\�����錋�ʂł��B</param>
        /// <param name="message">�e�X�g�̐����ł��B</param>
        private void CheckJudgeResult(
            int yourHand, int computerHand, int expectedResult, string message)
        {
            // �w��̈����Ń��\�b�h���Ăяo���A���ۂ̌��ʂ��\��������̂Ɠ������ǂ����m�F���܂��B
            int actualResult = JankenApp.JudgeResult(yourHand, computerHand);
            Assert.AreEqual(expectedResult, actualResult, message);
        }
    }
}
