namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdProgDir3Async(int top_prog = 1)
    {
        return await Task.FromResult(RdProgDir3(top_prog: top_prog));
    }

    public dynamic RdProgDir3(short type = 2, int top_prog = 1, short num_prog = 1)
    {
        var buf = new Focas.PRGDIR3();
        var top_prog_in = top_prog;
        var num_prog_in = num_prog;

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rdprogdir3(_handle, type, ref top_prog, ref num_prog, buf);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_rdprogdir3",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_rdprogdir3",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdprogdir3 = new {type, top_prog_in, num_prog_in}},
            response = new {cnc_rdprogdir3 = new {top_prog, num_prog, buf}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}