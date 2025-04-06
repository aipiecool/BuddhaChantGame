from flask import Blueprint, render_template

url_prefix = "/"
home = Blueprint("home", __name__, url_prefix=url_prefix)

# https://dwpan.com/s/NpH6

@home.route('/', methods=["GET"])
def index():
    return render_template('index.html')
