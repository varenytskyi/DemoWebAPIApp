var WebProxyBase = {
    baseUrl: '/api',
    _asyncJsonCallBase: function (url, method, data, callback, error) {
        var url = this.baseUrl + url;
        $.ajax({
            url: url,
            type: method,
            data: data,
            cache: false,
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
                if (callback && typeof callback === 'function')
                    callback(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (error && typeof error === 'function')
                    error(jqXHR, textStatus, errorThrown);
            }
        });
    },

    _jsonCallBase: function (url, method, data, error) {
        var url = this.baseUrl + url;
        var result;
        $.ajax({
            url: url,
            type: method,
            data: data,
            async: false,
            cache: false,
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                result = data;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (error && typeof error === 'function')
                    error(jqXHR, textStatus, errorThrown);
            }
        });
        return result;
    }
}