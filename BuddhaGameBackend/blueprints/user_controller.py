from flask import Blueprint

from ext import db
from model.BaseModel import datetime_now
from model.models import BrtGameUser
from utils.db_utils import db_query_first, db_add
from utils.file_utils import md5
from utils.flask_utils import *

url_prefix = "/login"
user = Blueprint("user", __name__, url_prefix=url_prefix)

# https://dwpan.com/s/NpH6

@user.route('/registerByPassword', methods=["POST"])
def register_by_password():
    params = http_auto_param_convert()
    try:
        username = params.get('username')
        password = params.get('password')
        ret = db_query_first(BrtGameUser, {
            'username': username,
        })
        if ret is not None:
            raise ValueError('该用户名已被注册')
        ret = db_add(BrtGameUser(
            **{
                'username': username,
                'password': md5(password),
                'player_info': '',
                'create_time': datetime_now(),
            }
        ))
        return return_success(data={
            "ID": ret.ID,
            "username": ret.username,
            "password": ret.password,
        })
    except Exception as e:
        print(e.__repr__())
        return return_fail(data=e.__repr__())


@user.route('/loginByPassword', methods=["POST"])
def login_by_password():
    params = http_auto_param_convert()
    try:
        username = params.get('username')
        password = params.get('password')
        ret = db_query_first(BrtGameUser, {
            'username': username,
        })
        if ret is None:
            raise ValueError('账号或密码错误')
        if ret.password != md5(password):
            raise ValueError('账号或密码错误')
        return return_success(data=ret.to_dict())
    except Exception as e:
        print(e.__repr__())
        return return_fail(data=e.__repr__())


@user.route('/queryUserPlayerInfo', methods=["POST"])
def query_user_player_info():
    params = http_auto_param_convert()
    try:
        userId = params.get('userId')
        password = params.get('password')
        ret = db_query_first(BrtGameUser, {
            'ID': userId,
            'password': password,
        })
        if ret is None:
            raise ValueError('登录超时')
        r = ret.to_dict()
        player_info = json.loads(r['player_info'])
        player_info['username'] = r['username']

        return json.dumps(player_info)
    except Exception as e:
        print(e.__repr__())
        return return_fail(data=e.__repr__())


@user.route('/saveUserPlayerInfo', methods=["POST"])
def save_user_player_info():
    params = http_auto_param_convert()
    try:
        userId = params.get('userId')
        password = params.get('password')
        player_info = params.get('playerInfo')
        ret = db_query_first(BrtGameUser, {
            'ID': userId,
            'password': password,
        })
        if ret is None:
            raise ValueError('登录超时')
        ret.player_info = player_info
        db.session.commit()
        return return_success(data='保存成功')
    except Exception as e:
        print(e.__repr__())
        return return_fail(data=e.__repr__())
