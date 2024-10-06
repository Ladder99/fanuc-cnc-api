namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdParaInfoAsync(short s_number, ushort read_no = 1)
    {
        return await Task.FromResult(RdParaInfo(s_number, read_no));
    }

    public dynamic RdParaInfo(short s_number, ushort read_no = 1)
    {
        var paraif = new Focas.ODBPARAIF();

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rdparainfo(_handle, s_number, read_no, paraif);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_rdparainfo",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/ncdata/cnc_rdparainfo",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdparainfo = new {s_number, read_no}},
            response = new {cnc_rdparainfo = new {paraif}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}