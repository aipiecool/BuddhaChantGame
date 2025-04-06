from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from sqlalchemy import create_engine

import static_config

db = SQLAlchemy()
app = Flask(__name__, static_folder="static", template_folder="templates")
app.config.from_object(static_config)
db.init_app(app)


def create_mysql_engine():
    return create_engine(static_config.DB_URI, echo=static_config.SQLALCHEMY_ECHO, future=True, pool_recycle=3600)
