namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdExecProgAsync(short length = 1024)
    {
        return await Task.FromResult(RdExecProg(length));
    }

    public dynamic RdExecProg(short length = 1024)
    {
        //length = 96;

        var data = new char[length];
        short blknum = 0;
        var length_out = (ushort) data.Length;

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rdexecprog(_handle, ref length_out, out blknum, data);
        });

        //string source = string.Join("", data).Trim();
        //string[] source_lines = source.Split('\n');

        /*
        int lc = 0;
        var t = DateTime.Now;
        foreach (var s in source_lines)
        {
            Console.WriteLine(t + " : " + lc + " : " + s);
            lc++;
        }
        */

        //Console.WriteLine("----------------------------");

        var nr = new
        {
            @null = false,
            method = "cnc_rdexecprog",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_rdexecprog",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdexecprog = new {length}},
            response = new {cnc_rdexecprog = new {length = length_out, blknum, data}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}