# import pickle
# import numpy as np
# import pandas as pd


# # Hardcoded player data
# player_data_dict = {
#     "playerId": 76561197969158018,
#     "player_games": 23,
#     "a_count": 1814.0,
#     "easy": 164.0,
#     "normal": 64.0,
#     "hard": 9.0,
#     "total_achieved": 237.0,
#     "percent_difficulty": 1043.412562,
#     "avg_score": 4.402585,
#     "total_time_played": 69435.0,
#     "avg_time_played": 3018.913043,
#     "game_score": 101.259447,
#     "percentage_completed": 13.065050
# }

# # Convert to dataframe since transformer expects a DataFrame
# player_data_df = pd.DataFrame([player_data_dict])

# # Drop the columns that were not used when training the model
# player_data_df = player_data_df.drop(columns=["playerId", "player_games", "easy", "normal", "hard"])

# # Load the transformer
# with open(r"C:\Users\matth\VSCode_Projects\Initial Difficulty Model\transformer.pkl", "rb") as file:
#     transformer = pickle.load(file)

# # Preprocess the player data
# preprocessed_data = transformer.transform(player_data_df)

# # Load the model
# with open(r"C:\Users\matth\VSCode_Projects\Initial Difficulty Model\model_cfg.h5", "rb") as file:
#     model = pickle.load(file)

# # Use the model to predict the player's difficulty level
# difficulty_prediction = model.predict(preprocessed_data)

# # Print the prediction
# print(f"Predicted difficulty for player {player_data_dict['playerId']}: {difficulty_prediction}")

# if difficulty_prediction == 1:
#     print("Difficulty should be set to normal (0)")
# elif difficulty_prediction == 2:
#     print("Difficulty should be set to hard (6)")
# else:
#     print("Difficulty should be set to easy (-7)")


import pickle
import numpy as np
import pandas as pd

def predict_difficulty(player_data_dict):
    # Convert to dataframe since transformer expects a DataFrame
    player_data_df = pd.DataFrame([player_data_dict])

    # Drop the columns that were not used when training the model
    player_data_df = player_data_df.drop(columns=["playerId", "player_games", "easy", "normal", "hard"])

    # Load the transformer
    with open(r"C:\Users\matth\VSCode_Projects\Initial Difficulty Model\transformer.pkl", "rb") as file:
        transformer = pickle.load(file)

    # Preprocess the player data
    preprocessed_data = transformer.transform(player_data_df)

    # Load the model
    with open(r"C:\Users\matth\VSCode_Projects\Initial Difficulty Model\model_cfg.h5", "rb") as file:
        model = pickle.load(file)

    # Use the model to predict the player's difficulty level
    difficulty_prediction = model.predict(preprocessed_data)

    return difficulty_prediction[0]
