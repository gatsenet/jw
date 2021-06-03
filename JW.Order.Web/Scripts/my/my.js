var getHearderTop = function (obj, noIOS) {
    var top = $(obj).offset().height;
   
    return top;
};

var isDefine = function (para) {
    if (typeof (para) == 'undefined' || para == '' || para == null || para == undefined)
        return false;
    else
        return true;
}; 

var $$ = function (id) {
    return $("#" + id)[0];
};

function toInt(s) {
    var _value = 0;
    if (isDefine(s)) {
        _value = parseInt(s);
        if (_value == NaN | isNaN(_value))
            _value = 0;
    }
    return _value;
}

function toFloat(s) {
    var _value = 0.00;
    if (isDefine(s)) {
        _value = parseFloat(s);
        if (_value == NaN | isNaN(_value))
            _value = 0.00;
    }
    return _value;
}

function toFloat2Point(x) {
    var f_x = toFloat(x);
    var f_x = Math.round(x * 100) / 100;
    var s_x = f_x.toString();
    var pos_decimal = s_x.indexOf('.');
    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }
    while (s_x.length <= pos_decimal + 2) {
        s_x += '0';
    }
    return s_x;
}

function toString(s) {
    return new String(isDefine(s) ? s : "");
}

function toBool(s) {
    var _val = toString(s).toLowerCase();
    if (_val == "true" | _val == "1")
        return true;
    else
        return false;
}

function stringToJson(str) {
    return JSON.parse(str);
}

function jsonToString(jsonObj) {
    return JSON.stringify(jsonObj);
}

/**
 * 给DOM对象赋值innerHTML
 * @param String id 对象id或者对象
 * @param String html html字符串
 * @param String showstr 当html不存在时的提示语
 */
function setHtml(id, html, showstr) {
    var showval = isDefine(showstr) ? showstr : "";
    if ("string" == typeof (id)) {
        var ele = $$(id);
        if (ele != null) {
            ele.innerHTML = isDefine(html) ? html : showval;
        } else {
            alert("没有id为" + id + "的对象");
        }
    } else if (id != null) {
        id.innerHTML = isDefine(html) ? html : showval;
    }
}

function getHtml(id) {
    if (isDefine(id)) {
        var ele = $$(id);
        if (ele != null) {
            return ele.innerHTML;
        } else {
            return "";
        }
    }
    else
        return "";
}

function entityToString(entity) {
    var div = document.createElement('div');
    div.innerHTML = entity;
    var res = div.innerText || div.textContent;
    //console.log(entity, '->', res);
    return res;
}

function toGetOnlineData(userdata, callback, errcallback, completecallback, timeout) {    
    if (toInt(timeout) == 0)
        timeout = 3 * 60 * 1000;
    var weburl = userdata.url;
    delete userdata["url"];
    $.ajax({
        url: weburl,
        type: 'POST',
        cache: false,
        timeout: timeout,
        data: userdata,        
        done: function (dataJson) {            
            callback(dataJson);            
        },
        fail: function (e) {            
            if (errcallback) {
                errcallback(e)
            }
            else {
                DevExpress.ui.notify(e.statusText, "error", 5000);
            }            
        },
        always: function (e) {
            if (completecallback) completecallback(e);
        }
    });
}

function toGetOnlineDataByPV(userdata, callback, isShowToast, button, timeout, errcallback, completecallback) {
    if (isShowToast == false) {
        $("#cisLoading").hide();
    }
    if (toInt(timeout) == 0)
        timeout = 3 * 60 * 1000;
    var weburl = userdata.url;
    delete userdata["url"];
    $.ajax({
        url: weburl,
        type: 'POST',
        cache: false,
        dataType: "html",
        timeout: timeout,
        data: userdata,
        beforeSend: function (XMLHttpRequest) {
            if (isShowToast == true) $("#cisLoading").show();
            if (isDefine(button)) $(button).attr("disabled", "disabled");
        },
        success: function (dataJson) {
            callback(dataJson);
        },
        error: function (e) {
            if (errcallback) errcallback(e);
            alert("连接失败,稍后重试");
        },
        complete: function (e) {
            if (isShowToast == true) $("#cisLoading").hide();
            if (isDefine(button)) $(button).removeAttr("disabled");
            if (completecallback) completecallback(e);
        }
    });
}