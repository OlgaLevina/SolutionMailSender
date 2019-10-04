using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailSenderLib.Service;

namespace MailSenderLib.Test.Service
{
    /// <summary>
    /// Сводное описание для ServiceEncoderTests
    /// </summary>
    [TestClass]
    public class ServiceEncoderTests
    {
        public ServiceEncoderTests()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Encode_ABC_to_BCD_with_key_1()
        {
            //Принцип ААА
            //А - arange - разместить исходные данные и результат, подготовить объект тестирования
            //A - act - соверщить ОДНО действие над объектом
            //A - Assert - сравнить полученный результат с заявленным в начале
            //1
            var str = "ABC";
            var expected_result = "BCD";
            var key = 1;
            //2
            var actual_result = StringEncoder.Encode(str, key);
            //3
            Assert.AreEqual(expected_result, actual_result);
        }
        [TestMethod]
        public void Decode_BCD_to_ABC_with_key_1()
        {
            var str = "BCD";
            var expected_result = "ABC";
            var key = 1;
            var actual_result = StringEncoder.Decode(str, key);
            Assert.AreEqual(expected_result, actual_result);
        }
    }
}
