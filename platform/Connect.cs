namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> ConnectAsync(string ip, ushort port, short timeout)
    {
        return await Task.FromResult(Connect(ip, port, timeout));
    }

    public dynamic Connect(string ip, ushort port, short timeout)
    {
        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_allclibhndl3(
                ip,
                port,
                timeout,
                out _handle);
        }, throwOnSocketError: false); // Exclude Connect from throwing if EW_SOCKET

        var nr = new
        {
            @null = false,
            method = "cnc_allclibhndl3",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/handle/cnc_allclibhndl3",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new
            {
                cnc_allclibhndl3 = new
                {
                    ipaddr = ip,
                    port = port,
                    timeout = timeout
                }
            },
            response = new {cnc_allclibhndl3 = new {FlibHndl = _handle}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}