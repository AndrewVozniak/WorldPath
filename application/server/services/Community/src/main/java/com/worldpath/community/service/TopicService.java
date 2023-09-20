package com.worldpath.community.service;

import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import com.worldpath.community.DTO.TopicDTO;
import com.worldpath.community.model.Topic;
import com.worldpath.community.repositories.TopicRepository;

import java.util.List;

@Service
public class TopicService {
    private final TopicRepository topicRepository;
    private final ModelMapper modelMapper;

    /**
     * Description: Constructor
     * @param topicRepository TopicRepository
     * @param modelMapper ModelMapper
     */
    public TopicService(TopicRepository topicRepository, ModelMapper modelMapper) {
        this.topicRepository = topicRepository;
        this.modelMapper = modelMapper;
    }

    /**
     * Description: Get topic by ID
     * @param id Topic ID
     * @return TopicDTO
     */
    public TopicDTO getTopicById(String id) {
        Topic topic = topicRepository.findById(id).orElse(null);

        TopicDTO topicDTO = modelMapper.map(topic, TopicDTO.class);

        assert topic != null;
        topicDTO.setUser_id(topic.getUserId());
        topicDTO.setCreated_at(topic.getCreatedAt());
        topicDTO.setUpdated_at(topic.getUpdatedAt());

        return topicDTO;
    }

    /**
     * Description: Create topic
     * @param topicDTO TopicDTO
     * @return TopicDTO
     */
    public TopicDTO createTopic(TopicDTO topicDTO) {
        Topic topic = modelMapper.map(topicDTO, Topic.class);
        topic.setUserId(topicDTO.getUser_id());

        Topic savedTopic = topicRepository.save(topic);

        TopicDTO savedTopicDTO = modelMapper.map(savedTopic, TopicDTO.class);
        savedTopicDTO.setUser_id(savedTopic.getUserId());
        savedTopicDTO.setCreated_at(savedTopic.getCreatedAt());
        savedTopicDTO.setUpdated_at(savedTopic.getUpdatedAt());

        return savedTopicDTO;
    }

    /**
     * Description: Get all topics
     * @return List of TopicDTO
     */
    public List<TopicDTO> getAllTopics() {
        List<Topic> topics = topicRepository.findAll();

        return topics.stream().map(topic -> {
            TopicDTO topicDTO = modelMapper.map(topic, TopicDTO.class);
            topicDTO.setUser_id(topic.getUserId());
            topicDTO.setCreated_at(topic.getCreatedAt());
            topicDTO.setUpdated_at(topic.getUpdatedAt());

            return topicDTO;
        }).toList();
    }

}
