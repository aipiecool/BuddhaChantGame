import os
from datetime import timedelta

from flask_cors import CORS

from blueprints.home_controller import home
from blueprints.user_controller import user
from ext import app

app.config['SECRET_KEY'] = os.urandom(24)
app.config['PERMANENT_SESSION_LIFETIME'] = timedelta(days=365)
CORS(app, supports_credentials=True)

app.register_blueprint(home)
app.register_blueprint(user)



def run():
    app.run(host="0.0.0.0", port=7519, debug=True)
