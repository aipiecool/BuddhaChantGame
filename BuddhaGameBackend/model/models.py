from model.BaseModel import *


class BrtGameUser(BaseModel):
    fields = []
    ID = createColumn(fields, "ID", DB_ID)
    username = createColumn(fields, "username", DB_Text)
    password = createColumn(fields, "password", DB_Text)
    player_info = createColumn(fields, "player_info", DB_Text)
    create_time = createColumn(fields, "create_time", DB_DATETIME)

