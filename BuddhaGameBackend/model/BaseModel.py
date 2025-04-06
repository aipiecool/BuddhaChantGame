from flask import request
from ext import db
from utils.date_utils import now_string

DB_ID = "ID"
DB_INTEGER = db.Integer
DB_FLOAT = db.Float
DB_SMALLINT = db.SmallInteger
DB_Text = db.Text
DB_LONG_Text = db.Text(16777216)
DB_VARCHAR256 = db.String(255)
DB_VARCHAR64 = db.String(64)
DB_DATETIME = db.DateTime


def createColumn(fields, name, typz, key=False, auto_inc=False, default=None, unique=False, nullable=True):
    fields.append(name)
    ret = None
    if typz == DB_ID:
        typz, key, auto_inc, unique, nullable = DB_INTEGER, True, True, True, False
    if default is not None:
        ret = db.Column(typz,
                        primary_key=key, autoincrement=auto_inc,
                        unique=unique, nullable=nullable, default=default)
    else:
        ret = db.Column(typz,
                        primary_key=key, autoincrement=auto_inc,
                        unique=unique, nullable=nullable)
    return ret


class BaseModel(db.Model):
    __abstract__ = True

    def to_dict(self, ret=None):
        if ret is None:
            ret = self
        fields = getattr(self, "fields")
        ret_data = {}
        for k in fields:
            v = getattr(ret, k)
            ret_data[k] = str(v)
        return ret_data


def entities_to_list(entities, add_col=None):
    result = []
    for d in entities:
        if isinstance(d, BaseModel):
            dic = d.to_dict()
            if add_col is not None:
                for i in range(len(add_col)):
                    dic[add_col[i]] = ''
            result.append(dic)
        else:
            dic = d[0].to_dict()
            for i in range(len(add_col)):
                if len(d) > i + 1:
                    dic[add_col[i]] = d[i + 1]
                else:
                    dic[add_col[i]] = None
            result.append(dic)
    return result


def datetime_now():
    return now_string()


def datetime_zero():
    return '0000-00-00 00:00:00'


def get_user_ip():
    ip_string = request.environ.get('HTTP_X_REAL_IP', request.remote_addr)
    if ip_string == "127.0.0.1":
        return request.remote_addr
    return ip_string
