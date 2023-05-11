import Axios from 'axios'

export abstract class PlayersService {
  static playerUrl = 'https://localhost:7187/Leaderboard'

  static async getPlayer(playerName: string): Promise<string> {
    const response = await Axios.get(this.playerUrl + '/GetPlayer?name=')
    return response.data
  }

  static async getTopPlayers(): Promise<string> {
    const response = await Axios.get(this.playerUrl + '/GetTopTen')
    return response.data
  }

  static async isExistingPlayer(playerName: string): Promise<boolean> {
    const response = await Axios.get(this.playerUrl + '/IsExistingPlayer?name=' + playerName)
    return response.data
  }
}
