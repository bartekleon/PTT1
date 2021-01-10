using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task2_Presentation;
using PT_Task2_Presentation_Model;
using System;

namespace PT_Task2_PresentationTest
{
    [TestClass]
    public class PresentationTest
    {
        private static int tstry = 1;

        [TestInitialize]
        public void BeforeAll()
        {
            DataSalvator.switchedOn = false;
        }

        [TestCleanup]
        public void AfterAll()
        {
            DataSalvator.switchedOn = true;
        }

        [TestMethod]
        public void VM_ConstructorTest()
        {
            Assert.IsFalse(DataSalvator.switchedOn);
            MyemViewModel vm = new MyemViewModel();

            Assert.IsNotNull(vm.AddEntryCommand);
            Assert.IsNotNull(vm.DeleteEntryCommand);
            Assert.IsNotNull(vm.IncreaseBookCountCommand);
            Assert.IsNotNull(vm.DecreaseBookCountCommand);
            Assert.IsNotNull(vm.FetchDataCommand);
            Assert.IsNotNull(vm.SaveDataCommand);
            Assert.AreEqual("Working!", vm.FeedbackMessage);

            Assert.IsNull(vm.Entries);
            Assert.IsNull(vm.HighlightedEntry);

            Assert.IsTrue(vm.FetchDataCommand.CanExecute(null));
            Assert.IsFalse(vm.AddEntryCommand.CanExecute(null));
            Assert.IsFalse(vm.DeleteEntryCommand.CanExecute(null));
            Assert.IsFalse(vm.IncreaseBookCountCommand.CanExecute(null));
            Assert.IsFalse(vm.DecreaseBookCountCommand.CanExecute(null));
            Console.WriteLine(tstry);
        }

        [TestMethod]
        public void VM_FetchTest()
        {
            Assert.IsFalse(DataSalvator.switchedOn);
            MyemViewModel vm = new MyemViewModel();

            vm.FetchDataCommand.Execute(null);
            Assert.IsNotNull(vm.Entries);
            Assert.IsTrue(vm.Entries.Count > 0);

            Assert.AreEqual(vm.Entries[0], vm.HighlightedEntry);
            Assert.IsTrue(vm.IncreaseBookCountCommand.CanExecute(null));
            Assert.IsTrue(vm.DeleteEntryCommand.CanExecute(null));
            Assert.IsTrue(vm.AddEntryCommand.CanExecute(null));
            Assert.AreEqual(vm.HighlightedEntry.BookCount > 0, vm.DecreaseBookCountCommand.CanExecute(null));

            foreach (Entry entry in vm.Entries)
            {
                Assert.IsNotNull(entry.Author);
                Assert.IsNotNull(entry.Title);
                Assert.IsNotNull(entry.BookCount);
            }
        }

        [TestMethod]
        public void VM_AddDeleteEntryTest()
        {
            Assert.IsFalse(DataSalvator.switchedOn);
            MyemViewModel vm = new MyemViewModel();

            vm.FetchDataCommand.Execute(null);
            int currentCount = vm.Entries.Count;

            vm.AddEntryCommand.Execute(null);
            Assert.AreEqual(currentCount + 1, vm.Entries.Count);
            Assert.AreEqual("", vm.HighlightedEntry.Title);
            Assert.AreEqual("", vm.HighlightedEntry.Author);
            Assert.AreEqual(0, vm.HighlightedEntry.BookCount);

            vm.DeleteEntryCommand.Execute(null);
            Assert.AreEqual(currentCount, vm.Entries.Count);
        }

        [TestMethod]
        public void VM_IncreaseDecreaseBookCountTest()
        {
            Assert.IsFalse(DataSalvator.switchedOn);
            MyemViewModel vm = new MyemViewModel();

            vm.FetchDataCommand.Execute(null);
            vm.AddEntryCommand.Execute(null);

            int currentCount = vm.HighlightedEntry.BookCount;
            vm.IncreaseBookCountCommand.Execute(null);
            Assert.AreEqual(currentCount + 1, vm.HighlightedEntry.BookCount);

            vm.DecreaseBookCountCommand.Execute(null);
            Assert.AreEqual(currentCount, vm.HighlightedEntry.BookCount);
        }

        [TestMethod]
        public void VM_CannotDecreaseBookCountBeyondZeroTest() {
            Assert.IsFalse(DataSalvator.switchedOn);
            MyemViewModel vm = new MyemViewModel();

            vm.FetchDataCommand.Execute(null);
            vm.AddEntryCommand.Execute(null);
            vm.IncreaseBookCountCommand.Execute(null);

            while (vm.HighlightedEntry.BookCount > 0) {
                Assert.IsTrue(vm.DecreaseBookCountCommand.CanExecute(null));
                vm.DecreaseBookCountCommand.Execute(null);
            }
            Assert.IsFalse(vm.DecreaseBookCountCommand.CanExecute(null));
        }
    }
}
