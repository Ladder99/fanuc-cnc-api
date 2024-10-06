namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> SvdtRdDataAsync(int length)
    {
        return await Task.FromResult(SvdtRdData(length));
    }

    public dynamic SvdtRdData(int length)
    {
        short stat = 0;
        var length_out = length;
        var data = new object();

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_svdtrddata(_handle, out stat, ref length_out, data);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_svdtrddata",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/servo/cnc_svdtrddata",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_svdtrddata = new {length}},
            response = new {cnc_svdtrddata = new {stat, length_out, data}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}