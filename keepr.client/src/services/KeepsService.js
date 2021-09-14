import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async getKeepById(id) {
    const found = AppState.keeps.find(k => k.id.toString() === id)
    if (found) {
      AppState.activeKeep = found
      return
    }
    const res = await api.get(`api/keeps/${id}`)
    logger.log(res.data)
    AppState.activeKeep = res.data
  }
}

export const keepsService = new KeepsService()
