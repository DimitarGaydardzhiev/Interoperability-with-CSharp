using System.Reflection;
using System.Runtime.InteropServices;

namespace Interopability
{
    internal class InteroperabilityConnector : IDisposable
    {
        private IntPtr libHandle;

        private SumDelegate? sumDelegate;

        private delegate long SumDelegate(long n1, long n2);

        private const string LibPath = "C:\\PathToYourCPlusPlusDll\\ExternalCPlusPlusLibrary.dll";

        public InteroperabilityConnector()
        {
            this.libHandle = NativeMethods.LoadLibrary(LibPath);
            this.sumDelegate = (SumDelegate?)GetExternalFunctionDelegate("CalculateSum", typeof(SumDelegate));
        }

        public long CalculateSum(long n1, long n2)
        {
            if (sumDelegate != null)
                return sumDelegate(n1, n2);

            throw new ArgumentNullException($"External method: {MethodBase.GetCurrentMethod().Name} not found!");
        }

        public void Dispose()
        {
            if (this.libHandle != IntPtr.Zero)
            {
                NativeMethods.FreeLibrary(this.libHandle);
                this.libHandle = IntPtr.Zero;
            }
        }

        private Delegate GetExternalFunctionDelegate(string functionName, Type type)
        {
            var pointer = NativeMethods.GetProcAddress(libHandle, functionName);

            if (pointer == IntPtr.Zero)
                throw new ArgumentNullException($"Function pointer for: {functionName} not found!");

            return Marshal.GetDelegateForFunctionPointer(pointer, type);
        }
    }
}