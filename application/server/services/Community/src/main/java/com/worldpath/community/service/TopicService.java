package com.worldpath.community.service;

import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import com.worldpath.community.DTO.TopicDTO;
import com.worldpath.community.model.Topic;
import com.worldpath.community.repositories.TopicRepository;

import com.worldpath.community.action.ConvertToTopicDTOAction;

import java.util.List;

@Service
public class TopicService {
    private final TopicRepository topicRepository;
    private final ConvertToTopicDTOAction convertToTopicDTOAction;
    private final ModelMapper modelMapper;


    /**
     * @Description Constructor
     * @param topicRepository TopicRepository
     * @param modelMapper ModelMapper
     */
    public TopicService(TopicRepository topicRepository, ModelMapper modelMapper) {
        this.topicRepository = topicRepository;
        this.modelMapper = modelMapper;
        this.convertToTopicDTOAction = new ConvertToTopicDTOAction(modelMapper);
    }

    /**
     * @Description Get topic by ID
     * @param id Topic ID
     * @return TopicDTO
     */
    public TopicDTO getTopicById(String id) {
        Topic topic = topicRepository.findById(id).orElse(null);

        if (topic == null) {
            return null;
        }

        return convertToTopicDTOAction.execute(topic);
    }

    /**
     * @Description Create topic
     * @param topicDTO TopicDTO
     * @return TopicDTO
     */
    public TopicDTO createTopic(TopicDTO topicDTO) {
        Topic topic = modelMapper.map(topicDTO, Topic.class);
        topic.setUserId(topicDTO.getUser_id());
        topic.createTopicDate();

        Topic savedTopic = topicRepository.save(topic);

        return convertToTopicDTOAction.execute(savedTopic);
    }

    /**
     * @Description Update topic by ID
     * @param topicDTO TopicDTO
     * @param topicID Topic ID
     * @return TopicDTO
     */
    public TopicDTO updateTopic(TopicDTO topicDTO, String topicID) {
        // ? Find topic by ID
        Topic topic = topicRepository.findById(topicID).orElse(null);

        // ? If topic is null or user ID is not equal to topic's user ID, return null
        if (topic == null) {
            return null;
        }

        // ? If user ID is not equal to topic's user ID, return null
        if (topicDTO.getUser_id() != null && !topicDTO.getUser_id().equals(topic.getUserId())) {
            return null;
        }

        // ? Update topic fields
        topic.updateTopic(topicDTO.getTitle(), topicDTO.getDescription());

        // ? Save topic to database
        Topic savedTopic = topicRepository.save(topic);

        // ? Map saved topic to TopicDTO
        return convertToTopicDTOAction.execute(savedTopic);
    }

    /**
     * @Description  Get all topics
     * @return List of TopicDTO
     */
    public List<TopicDTO> getAllTopics() {
        List<Topic> topics = topicRepository.findAll();

        return topics.stream().map(convertToTopicDTOAction::execute).toList();
    }
}
