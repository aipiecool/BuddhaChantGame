import json
from collections import OrderedDict

from flask import request


def http_get_param_convert():
    items = request.args.items()
    params = OrderedDict()
    for item in items:
        params[item[0]] = item[1]
    return params


def http_post_param_convert():
    items = request.form.items()
    params = OrderedDict()
    for item in items:
        params[item[0]] = item[1]
    return params


def http_auto_param_convert():
    params = []
    if request.method == 'POST':
        if request.is_json:
            params = request.json
        else:
            params = http_post_param_convert()
    else:
        params = http_get_param_convert()
    return HttpParams(params)


def return_success(code=1, data='success'):
    return json.dumps({'code': code, 'data': data})


def return_fail(code=-1, data='internal error'):
    return json.dumps({'code': code, 'data': data})


class NoParamsException(Exception):
    def __init__(self, name):
        self.name = name

    def __repr__(self):
        msg = 'NoParams:{}'.format(self.name)
        return msg

    def __str__(self):
        return self.__repr__()


class HttpParams:
    def __init__(self, params):
        self.params = params

    def get(self, key, default=None):
        if key in self.params:
            return self.params[key]
        if default is not None:
            return default
        raise NoParamsException(key)
