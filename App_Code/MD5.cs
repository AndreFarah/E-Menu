using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for MD5
/// </summary>
public class MD5Encoding
{
    public MD5Encoding()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string ComputeMD5(string value)
    {
        /*
     * Create the md5 crypt service provider
     */
        MD5 crypt = new MD5CryptoServiceProvider();
        StreamWriter pwd = new StreamWriter(new MemoryStream());
        pwd.Write(value);
        pwd.Flush();

        /*
         * Compute the hash code
         */
        pwd.BaseStream.Seek(0, SeekOrigin.Begin);
        byte[] cryptHash = crypt.ComputeHash(pwd.BaseStream);

        /*
         * Convert the result to hex 
         */
        string result = "";

        int nLen = cryptHash.Length;
        for (int nPos = 0; nPos < nLen; nPos++)
        {
            byte cBuff = cryptHash[nPos];
            result += Convert((long)cBuff, 16);
        }

        return result.ToLower();
    }

    /// <summary>
    /// Converts a number into different bases
    /// </summary>
    /// <param name="dblCount">Value to convert</param>
    /// <param name="intBaseformat">Numberbase e.x. 16 as Hex</param>
    /// <returns>converted number as a string</returns>
    private static string Convert(long dblCount, int intBaseformat)
    {
        string result = "";
        int potenz = 1;
        long temp;
        long pow;

        while (dblCount / (long)Math.Pow(intBaseformat, potenz) >= intBaseformat) potenz++;

        while (potenz >= 0)
        {
            if (potenz == 0)
            {
                temp = dblCount;
                dblCount = 0;
            }
            else
            {
                pow = (long)Math.Pow(intBaseformat, potenz);
                temp = dblCount / pow;
                dblCount = dblCount - (temp * pow);
            }

            if (temp < 10)
                result += temp.ToString();
            else
                result += (char)('A' + (temp - 10));

            potenz--;
        }

        if (result == "") result = "0";
        return result;
    }
}
