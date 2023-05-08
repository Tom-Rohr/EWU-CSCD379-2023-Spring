import Axios from 'axios'

export abstract class PlayersService {
  static wordUrl = 'https://wordleclone2023.azurewebsites.net/players'

  static async getTopPlayers(): Promise<string> {
    const response = await Axios.get(this.wordUrl)
    return response.data
  }
}
