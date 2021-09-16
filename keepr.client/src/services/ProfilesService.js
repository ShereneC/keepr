import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfileById(id) {
    try {
      const res = await api.get('api/profiles/' + id)
      AppState.profile = res.data
    } catch (err) {
      logger.error('Profile Does Not Exist', err)
    }
  }
}

export const profilesService = new ProfilesService()
